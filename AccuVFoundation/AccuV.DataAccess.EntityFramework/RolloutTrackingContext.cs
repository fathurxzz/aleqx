using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AccuV.DataAccess.Contracts;
using AccuV.DataAccess.Entities;
using AccuV.DataAccess.EntityFramework.Mapping;
using Task = AccuV.DataAccess.Entities.Task;

namespace AccuV.DataAccess.EntityFramework
{
    public partial class RolloutTrackingContext : DbContext, IDataSession
    {
        static RolloutTrackingContext()
        {
            Database.SetInitializer<RolloutTrackingContext>(null);
        }

        public RolloutTrackingContext()
            : base("Name=Rollout_TrackingContext")
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RolloutTracking> RolloutTracking { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ProjectLastModified>  ProjectsLastModified { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<PermissionsAlias> PermissionsAliases { get; set; }
        public DbSet<GCType> GcTypes { get; set; }
        public DbSet<GeneralContractor> GeneralContractors { get; set; }
        public DbSet<GCSite> GcSites { get; set; }
        public DbSet<DocumentEntry> DocumentEntries { get; set; }
        public DbSet<UploadedDocument> UploadedDocuments { get; set; }
        public DbSet<RequiredDocument> RequiredDocuments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }        
        public  DbSet<SecurityView> SecurityViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ManagerMap());
            modelBuilder.Configurations.Add(new MarketMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new RolloutTrackingMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new TaskCommentMap());
            modelBuilder.Configurations.Add(new ProjectLastModifiedMap());
            modelBuilder.Configurations.Add(new GcTypeMap());
            modelBuilder.Configurations.Add(new GeneralContractorMap());
            modelBuilder.Configurations.Add(new GcSiteMap());
            modelBuilder.Configurations.Add(new DocumentEntryMap());
            modelBuilder.Configurations.Add(new RequiredDocumentMap());
            modelBuilder.Configurations.Add(new UploadedDocumentMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ActivityMap()); 
            modelBuilder.Configurations.Add(new UserActivityMap());
            modelBuilder.Configurations.Add(new ActivityTypeMap());            
            modelBuilder.Configurations.Add(new ModuleMap());
        }



        public async Task<IEnumerable<IDictionary<string, object>>> GetCloseoutPackageViewData(CloseoutpackageViewRequest request)
        {
            const string itemSeparator = "$$$";
            var parameters = new List<SqlParameter>();
                
            parameters.Add(new SqlParameter("p_proj_id", request.ProjectId));

            if (request.MarketManager.Length > 0)
                parameters.Add(new SqlParameter("p_mm", string.Join(itemSeparator, request.MarketManager)));
            if (request.ConstructionManager.Length > 0)
                parameters.Add(new SqlParameter("p_cm", string.Join(itemSeparator, request.ConstructionManager)));
            if (request.Region.Length > 0)
                parameters.Add(new SqlParameter("p_region", string.Join(itemSeparator, request.Region)));
            if (request.Market.Length > 0)
                parameters.Add(new SqlParameter("p_market", string.Join(itemSeparator, request.Market)));
            if (request.Site.Length > 0)
                parameters.Add(new SqlParameter("p_site", string.Join(itemSeparator, request.Site)));
            if (request.Task.Length > 0)
                parameters.Add(new SqlParameter("p_task", string.Join(itemSeparator, request.Task)));
            if (!string.IsNullOrEmpty(request.GeneralContractor))
                parameters.Add(new SqlParameter("p_gc", request.GeneralContractor));
            if (request.DocumentId.Length > 0)
                parameters.Add(new SqlParameter("p_doc_id", string.Join(itemSeparator, request.DocumentId)));
            if (request.DocumentType.Length > 0)
                parameters.Add(new SqlParameter("p_doc_type", string.Join(itemSeparator, request.DocumentType)));
            if (request.NotApplicable.HasValue)
                parameters.Add(new SqlParameter("p_not_applicable", request.NotApplicable));
            parameters.Add(new SqlParameter("p_admin", request.IsAdmin));
            parameters.Add(new SqlParameter("p_page", request.Page));

            return await Database.SqlQuery<Dictionary<string, object>>(
                DbUtility.DbUtility.GenerateProcedureCall("usp_standalone_list", parameters), parameters.ToArray()).ToListAsync();
        }

        public async Task<List<UserProjectModuleClaim>> GetUserProjectModules(string userName)
        {
            string query = "SELECT DISTINCT project_id 'ProjectId',module_description 'Module' FROM [dbo].[security_view]";
            query += " WHERE [security_view].[usr_id] = @username";
            return  await Database.SqlQuery<UserProjectModuleClaim>(query, new SqlParameter("@username", userName)).ToListAsync();
        }

        void IDisposable.Dispose()
        {
          //  SaveChanges();
        }
    }
}
