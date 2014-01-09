using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AccuV.DataAccess;
using AccuV.DataAccess.Entities;
using AccuVAPI.Contracts;
using AccuVAPI.Dto;
using AccuVAPI.Utilities;
using BoxApi.V2.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccuVAPI.Operations
{
    #region Helper DTO Classes

    public class UpdateMessage
    {
        public const string OK = "UPDATE-0000";
        public const string WARNING = "UPDATE-0010";
        public const string ERROR = "UPDATE-0100";

        public string Code { get; set; }
        public string Description { get; set; }

        public UpdateMessage()
        {
            Code = string.Empty;
            Description = string.Empty;
        }

    }

    public class UpdateResult
    {
        public List<object> ModifiedRecords { get; set; }
        public List<UpdateMessage> Messages { get; set; }

        public UpdateResult()
        {
            ModifiedRecords = new List<object>();
            Messages = new List<UpdateMessage>();
        }
    }

    public class FilterResult
    {
        public const string USER_ADMIN = "admin";
        public const string USER_MARKET_MANAGER = "market";
        public const string USER_CONSTRUCTION_MANAGER = "construction";

        public const string OK = "FILTER-0000";
        public const string USER_NOT_FOUND = "FILTER-0010";
        public const string ERROR = "FILTER-0100";

        public string Code { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public List<string> Data { get; set; }

        public FilterResult()
        {
            Code = FilterResult.OK;
            Description = string.Empty;
            Properties = new Dictionary<string, object>();
            Data = new List<string>();
        }
    }

    #endregion


    public class SiteOperationsOld
    {
        private CommentOperations _commentOperations;
        private readonly IDataSession _session;


        public SiteOperationsOld(IDataSession session, CommentOperations commentOperations)
        {
            _commentOperations = commentOperations;
            _session = session;
        }

        public int PageSize { get; set; }

        public SiteResultSet Filter(SiteFilterSet filterInfo)
        {
            var result = new SiteResultSet();

            // extract the sites repository
            IEnumerable<Site> sites = _session.Sites//.Include(s=>s.RolloutTracking).AsNoTracking()
                .Where(s => filterInfo.ProjectId.Equals(s.ProjectId))
                .Where(
                    s =>
                        (filterInfo.MarketManager.Any())
                            ? filterInfo.MarketManager.Contains(s.Market.Manager.ManagerName.TrimEnd())
                            : s != null)
                .Where(
                    s =>
                        (filterInfo.ConstructionManager.Any())
                            ? filterInfo.ConstructionManager.Contains(s.Manager.ManagerName.TrimEnd())
                            : s != null)
                .Where(
                    s => (filterInfo.Region.Any()) ? filterInfo.Region.Contains(s.Market.Region.TrimEnd()) : s != null)
                .Where(
                    s => (filterInfo.Market.Any()) ? filterInfo.Market.Contains(s.Market.MarketId.TrimEnd()) : s != null)
                .Where(s => (filterInfo.Site.Any()) ? filterInfo.Site.Contains(s.SiteNumber.TrimEnd()) : s != null);

            result.Total = sites.Count();

            int skip = (filterInfo.Page - 1) * PageSize;
            int take = PageSize;

            sites = sites.Skip(skip).Take(take);

            _session.RolloutTracking
                .Where(rt => filterInfo.Task.Any() && filterInfo.Task.Contains(rt.TaskId))
                .Where(rt => sites.Contains(rt.Site)).ToList();

            result.Sites = sites;

            return result;
        }

        public UpdateResult Update(string projectId, string userName, Dictionary<string, object> modifiedRecords)
        {
            UpdateResult result = new UpdateResult();

            // before proceeding with the update
            // we should check if the DB is locked
            UpdateMessage dbLock = GetDbStatus(projectId);

            if (dbLock.Code == UpdateMessage.OK)
            {
                foreach (KeyValuePair<string, object> rowPair in modifiedRecords)
                {
                    Dictionary<string, object> row = JsonConvert.DeserializeObject<Dictionary<string, object>>(rowPair.Value.ToString());

                    IEnumerable<string> modifiedTasksFullName = row.Keys.Where(prop => prop.StartsWith("Task")).Distinct();
                    string modifiedSite = rowPair.Key;

                    foreach (string modifiedTask in modifiedTasksFullName)
                    {
                        DateTime? date = (DateTime?)row[modifiedTask];

                        UpdateMessage message = UpdateTaskDate(date, modifiedTask, modifiedSite, userName, projectId);

                        switch (message.Code)
                        {
                            case UpdateMessage.OK:
                                result.ModifiedRecords.Add(rowPair.Value.ToString());
                                break;
                            case UpdateMessage.WARNING:
                                result.ModifiedRecords.Add(rowPair.Value.ToString());
                                result.Messages.Add(message);
                                break;
                            case UpdateMessage.ERROR:
                                result.Messages.Add(message);
                                break;
                        }
                    }
                }

                if (result.ModifiedRecords.Any())
                    _session.SaveChanges();

            }
            else
            {
                // The DB is locked
                result.Messages.Add(dbLock);
            }

            return result;
        }

        //TODO: move to another operations class
        #region Sites FILTERS

        public FilterResult GetMarketManagers(string userId, string userLevel, string projectId)
        {
            FilterResult result = new FilterResult();

            result.Properties["enabled"] = false;


            //TODO: do it in a single query

            IEnumerable<Market> markets =
                _session.Markets.Where(
                    m =>
                        m.Sites.Any(
                            s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase)))
                    .Distinct();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                if (userLevel == FilterResult.USER_ADMIN)
                {
                    result.Properties["userLevel"] = FilterResult.USER_ADMIN;
                    result.Properties["enabled"] = true;
                    result.Data = markets.Select(m => m.Manager.ManagerName).Distinct().OrderBy(m => m).ToList();
                }
                else
                {
                    // User is Not Admin
                    // But check if it has an Alias
                    // If any Manager aliases are found, they are kept in the "userAliasManagers" member
                    // otherwise, the "userAliasManagers" will contain only the current user ID
                    IEnumerable<string> userAliasManagers = _session.PermissionsAliases.Where(p => p.AliasUserName.Equals(userId, StringComparison.InvariantCultureIgnoreCase)).Select(p => p.ManagerUserName.ToLower()).ToList();
                    if (userAliasManagers == null || !userAliasManagers.Any())
                        userAliasManagers = new List<string> { userId };

                    // Search for the logged in user inside the [Market] table
                    // and determine if he is a Market Manager
                    var isMarketManager = false;
                    foreach (string userAliasManager in userAliasManagers)
                    {
                        Market userMarket = markets.Where(m => m.ManagerId.Equals(userAliasManager, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (userMarket != null)
                        {
                            var marketManagerName = userMarket.ManagerId;

                            isMarketManager = true;

                            result.Properties["userLevel"] = FilterResult.USER_MARKET_MANAGER;
                            result.Properties["userAliasManager"] = userAliasManager;

                            var marketManager = _session.Managers.Where(m => m.ManagerId.Equals(userAliasManager, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                            
                            if (marketManager != null)
                            {
                                marketManagerName = marketManager.ManagerName;
                            }

                            result.Data.Add(marketManagerName);
                            break;
                        }
                    }

                    // in case the user was not found through the market managers,
                    // he will be considered as a construction manager
                    if (!isMarketManager)
                    {
                        result.Properties["userLevel"] = FilterResult.USER_CONSTRUCTION_MANAGER;
                    }
                }
            }

            return result;
        }

        public FilterResult GetConstructionManagers(string userId, string userLevel, string projectId)
        {
            FilterResult result = new FilterResult();
            result.Properties["enabled"] = false;

            if (!string.IsNullOrWhiteSpace(userLevel))
            {
                switch (userLevel)
                {
                    case FilterResult.USER_ADMIN:
                        result.Properties["enabled"] = true;
                        result.Data = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase))
                            .Select(s => s.Manager.ManagerName)
                            .Distinct()
                            .OrderBy(s => s)
                            .ToList<string>();
                        break;

                    case FilterResult.USER_MARKET_MANAGER:
                        result.Properties["enabled"] = true;
                        result.Data = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && s.Market.ManagerId.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                            .Select(s => s.Manager.ManagerName)
                            .Distinct()
                            .OrderBy(s => s)
                            .ToList<string>();
                        break;

                    case FilterResult.USER_CONSTRUCTION_MANAGER:

                        // But check if the user has an Alias
                        // If any Manager aliases are found, they are kept in the "userAliasManagers" member
                        // otherwise, the "userAliasManagers" will contain only the current user ID
                        IEnumerable<string> userAliasManagers = _session.PermissionsAliases.Where(p => p.AliasUserName.Equals(userId, StringComparison.InvariantCultureIgnoreCase)).Select(p => p.ManagerUserName.ToLower()).ToList<string>();
                        if (userAliasManagers == null || userAliasManagers.Count() == 0)
                            userAliasManagers = new List<string> { userId };

                        var isConstructionManager = false;
                        foreach (string userAliasManager in userAliasManagers)
                        {
                            Site userSite = _session.Sites.Where(m => m.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && m.ManagerId.Equals(userAliasManager, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                            if (userSite != null)
                            {
                                isConstructionManager = true;
                                result.Properties["userAliasManager"] = userAliasManager;
                                result.Data.Add(userSite.Manager.ManagerName);
                                break;
                            }
                        }

                        if (!isConstructionManager)
                        {
                            result.Code = FilterResult.ERROR;
                            result.Description = "No data returned from server.";
                        }

                        break;
                }
            }

            return result;
        }

        public IEnumerable<string> GetDistinctRegions(string userId, string userLevel, string projectId)
        {

            IEnumerable<string> result = new List<string>();

            IEnumerable<string> marketIdBase = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.MarketId)
                .Distinct();
            IEnumerable<Market> marketBase = _session.Markets.Where(m => marketIdBase.Contains(m.MarketId.TrimEnd())).Distinct();

            switch (userLevel)
            {
                case FilterResult.USER_ADMIN:
                    result = marketBase
                        .Select(m => m.Region.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m);
                    break;
                case FilterResult.USER_MARKET_MANAGER:
                    result = marketBase.Where(m => m.ManagerId.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                        .Select(m => m.Region.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m);
                    break;
                case FilterResult.USER_CONSTRUCTION_MANAGER:
                    var userSites = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && s.ManagerId.TrimEnd().Equals(userId, StringComparison.InvariantCultureIgnoreCase));
                    result = userSites.Select(s => s.Market.Region.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m);
                    break;
            }

            return result;
        }

        public IEnumerable<string> GetDistinctMarkets(string userId, string userLevel, string projectId)
        {
            IEnumerable<string> result = new List<string>();

            IEnumerable<string> marketIdBase = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.MarketId)
                .Distinct();
            IEnumerable<Market> marketBase = _session.Markets.Where(m => marketIdBase.Contains(m.MarketId.TrimEnd())).Distinct();

            switch (userLevel)
            {
                case FilterResult.USER_ADMIN:
                    result = marketBase.Select(m => m.MarketId.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m).ToList<string>();
                    break;
                case FilterResult.USER_MARKET_MANAGER:
                    result = marketBase.Where(m => m.ManagerId.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                        .Select(m => m.MarketId.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m).ToList<string>();
                    break;
                case FilterResult.USER_CONSTRUCTION_MANAGER:
                    var userSites = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && s.ManagerId.TrimEnd().Equals(userId, StringComparison.InvariantCultureIgnoreCase));
                    result = userSites.Select(s => s.Market.MarketId.TrimEnd())
                        .Distinct()
                        .OrderBy(m => m);
                    break;
            }

            return result;
        }

        public IEnumerable<string> GetDistinctTasks(string projectId)
        {
            var distinctTasks = _session.Tasks
                .Where(t => t.ProjectId.TrimEnd().Equals(projectId))
                .Select(s => s.TaskId.TrimEnd())
                .Distinct();
            return distinctTasks.AsEnumerable<string>();
        }

        public IEnumerable<string> GetDistinctSites(string userId, string userLevel, string projectId)
        {
            IEnumerable<string> result = new List<string>();

            switch (userLevel)
            {
                case FilterResult.USER_ADMIN:
                    result = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase))
                                .Select(s => s.SiteNumber.TrimEnd()).Distinct().ToList<string>();
                    break;

                case FilterResult.USER_MARKET_MANAGER:
                    result = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && s.Market.ManagerId.TrimEnd().Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                        .Select(s => s.SiteNumber.TrimEnd())
                        .Distinct().ToList<string>();
                    break;

                case FilterResult.USER_CONSTRUCTION_MANAGER:
                    var userSites = _session.Sites.Where(s => s.ProjectId.TrimEnd().Equals(projectId, StringComparison.InvariantCultureIgnoreCase) && s.ManagerId.Equals(userId, StringComparison.InvariantCultureIgnoreCase));
                    result = userSites.Select(s => s.SiteNumber.TrimEnd()).Distinct();
                    break;
            }

            return result;
        }

        #endregion

        #region HELPERS

        private UpdateMessage UpdateTaskDate(DateTime? date, string taskName, string siteNumber, string userName, string projectId)
        {
            UpdateMessage message = new UpdateMessage();
            message.Code = UpdateMessage.OK;

            try
            {

                taskName = XmlConvert.DecodeName(taskName);
                string taskId = taskName.Replace("Task ", "").Replace(" Actual", "").Replace(" Forecasted", "");

                List<RolloutTracking> allTasksPerSite = _session.RolloutTracking.Where(rt => rt.SiteNumber.Trim().Equals(siteNumber))
                    .OrderBy(rt => rt.TaskId).ToList();

                if (date.HasValue)
                {
                    // Set the time of the updated date to be always at noon
                    // This will help us avoid issues with time zones
                    TimeSpan noon = new TimeSpan(12, 0, 0);
                    date = ((DateTime)date).Date + noon;

                    if (taskName.EndsWith("Forecasted"))
                    {
                        // Validte the FORECASTED date modification

                        // RULE = "Date is in the past - error"
                        if (message.Code == UpdateMessage.OK)
                        {
                            DateTime today = DateTime.Now;
                            // By making a .Date comparison between 2 DateTime objects, the time will be ignored
                            if (today.Date > date.Value.Date)
                            {
                                message.Code = UpdateMessage.ERROR;
                                message.Description = string.Format("Site {0} / Task {1} - Forecasted date cannot be in the past.", siteNumber, taskId);
                            }
                        }

                        // RULE = "Task has an actual date - error"
                        if (message.Code == UpdateMessage.OK)
                        {
                            RolloutTracking modifiedSite = allTasksPerSite.Where(rt => rt.TaskId.TrimEnd().Equals(taskId)).FirstOrDefault();
                            if (modifiedSite == null)
                            {
                                message.Code = UpdateMessage.ERROR;
                                message.Description = string.Format("Site {0} is not associated with Task {1}", siteNumber, taskId);
                            }
                            else if (modifiedSite.CompletedDate != null)
                            {
                                message.Code = UpdateMessage.ERROR;
                                message.Description = string.Format("Site {0} / Task {1} - Forecasted date cannot be modified because the Task has already a valid Actual date.", siteNumber, taskId);
                            }
                        }

                        // RULE = "Future date exceeds forecast date of downstream tasks - warning"
                        if (message.Code == UpdateMessage.OK)
                        {
                            IEnumerable<RolloutTracking> upStreamTasks = allTasksPerSite.Where(rt => string.Compare(rt.TaskId.TrimEnd(), taskId) > 0);
                            foreach (RolloutTracking rt in upStreamTasks)
                            {
                                // By making a .Date comparison between 2 DateTime objects, the time will be ignored
                                if (rt.ForecastDate != null && date.Value.Date > ((DateTime)rt.ForecastDate).Date)
                                {
                                    message.Code = UpdateMessage.WARNING;
                                    message.Description = string.Format("Site {0} / Task {1} - The selected Forecast date is greater than Task {2} ({3}) ",
                                                                        siteNumber, taskId, rt.TaskId.TrimEnd(), ((DateTime)rt.ForecastDate).ToShortDateString());
                                    break;
                                }
                            }
                        }

                        // RULE = "Future date is within 3 days of downstream tasks - warning"
                        if (message.Code == UpdateMessage.OK)
                        {
                            IEnumerable<RolloutTracking> upStreamTasks = allTasksPerSite.Where(rt => string.Compare(rt.TaskId.Trim(), taskId) > 0);
                            foreach (RolloutTracking rt in upStreamTasks)
                            {
                                if (rt.ForecastDate != null
                                    && date.Value.Date < ((DateTime)rt.ForecastDate).Date
                                    && date.Value.Date >= ((DateTime)rt.ForecastDate).AddDays(3).Date)
                                {
                                    message.Code = UpdateMessage.WARNING;
                                    message.Description = string.Format("Site {0} / Task {1} - The selected Forecast date is within 3 days of Task {2} ({3}) ",
                                                                        siteNumber, taskId, rt.TaskId.TrimEnd(), ((DateTime)rt.ForecastDate).ToShortDateString());
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Validate the ACTUAL date modification

                        // RULE = "Date is in the future - error"
                        if (message.Code == UpdateMessage.OK)
                        {
                            DateTime today = DateTime.Now;
                            if (today.Date < date.Value.Date)
                            {
                                message.Code = UpdateMessage.ERROR;
                                message.Description = string.Format("Site {0} / Task {1} - Actual date cannot be in the future.", siteNumber, taskId);
                            }
                        }

                        // RULE = "Predecessor has no actual date - warning"
                        if (message.Code == UpdateMessage.OK)
                        {
                            IEnumerable<RolloutTracking> downStreamNullActualTasks = allTasksPerSite.Where(rt => string.Compare(rt.TaskId.Trim(), taskId) < 0 && rt.CompletedDate == null);
                            if (downStreamNullActualTasks.Count() > 0)
                            {
                                message.Code = UpdateMessage.WARNING;
                                message.Description = string.Format("Site {0} / Task {1} - Not all the previous Milestones have been completed ({2})",
                                                                    siteNumber, taskId, downStreamNullActualTasks.Select(rt => rt.TaskId.TrimEnd()).ToArray<string>().Aggregate((i, j) => i + "," + j));
                            }
                        }
                    }
                }

                // Validation has finished


                // After validation, mark the modified DB entity as modified
                // Also, add an automated comment 

                if (message.Code == UpdateMessage.OK || message.Code == UpdateMessage.WARNING)
                {
                    // Mark the DB Entity as Modified
                    RolloutTracking modifiedEntity = allTasksPerSite.FirstOrDefault(rt => rt.TaskId.TrimEnd().Equals(taskId));

                    if (taskName.EndsWith("Actual"))
                    {
                        AddAutomaticComment(siteNumber, taskId, "ACTUAL", modifiedEntity.CompletedDate, date, userName, projectId);
                        modifiedEntity.CompletedDate = date;
                    }
                    else if (taskName.EndsWith("Forecasted"))
                    {
                        AddAutomaticComment(siteNumber, taskId, "FORECASTED", modifiedEntity.ForecastDate, date, userName, projectId);
                        modifiedEntity.ForecastDate = date;
                    }

                    //_session.Entry(modifiedEntity).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                message.Code = UpdateMessage.ERROR;
                message.Description = "An unknown error has occured while updating.";
            }

            return message;
        }

        private UpdateMessage GetDbStatus(string projectId)
        {
            UpdateMessage dbStatus = new UpdateMessage();

            var lastModified = _session.ProjectsLastModified.Where(pr => pr.ProjectId.Equals(projectId)).OrderByDescending(pr => pr.LastModified).FirstOrDefault();
            if (lastModified != null)
            {
                if (!lastModified.DumpCreated)
                {
                    dbStatus.Code = UpdateMessage.OK;
                }
                else
                {
                    dbStatus.Code = UpdateMessage.ERROR;
                    dbStatus.Description = "Tracker updates are not permitted at this time. Updates can be made once the data is refreshed in the morning.";
                }
            }

            return dbStatus;
        }

        //TODO: Doesn't seem that this belongs here.
        private void AddAutomaticComment(string siteNumber, string taskId, string commentOn, DateTime? dateBeforeUpdate, DateTime? dateAfterUpdate, string userName, string projectId)
        {
            var automatedComment = new TaskComment();
            automatedComment.ProjectId = projectId;
            automatedComment.UserId = userName;
            automatedComment.CommentOn = commentOn;
            automatedComment.SiteNumber = siteNumber;
            automatedComment.TaskId = taskId;

            string from = (dateBeforeUpdate != null) ? ((DateTime)dateBeforeUpdate).ToShortDateString() : "null";
            string to = (dateAfterUpdate != null) ? ((DateTime)dateAfterUpdate).ToShortDateString() : "null";
            automatedComment.Comment = string.Format(SystemResources.SystemComment, from, to);


            _commentOperations.Insert(automatedComment);
        }
        #endregion

    }
}
