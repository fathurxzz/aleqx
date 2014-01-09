using System;
using System.Collections.Generic;
using System.Linq;
using AccuV.DataAccess;
using AccuV.DataAccess.Entities;

namespace AccuVAPI.Operations
{
    public class CommentOperations : IOperationStore
    {
        private readonly IDataSession _session;

        public CommentOperations(IDataSession session)
        {
            _session = session;
        }

        public IEnumerable<TaskComment> Get(string taskId, string commentOn, string siteNumber)
        {
            IEnumerable<TaskComment> allComments = _session.TaskComments
                .Where(c => (c.SiteNumber.Equals(siteNumber, StringComparison.InvariantCultureIgnoreCase)
                                    && c.TaskId.Equals(taskId, StringComparison.InvariantCultureIgnoreCase)
                                    && c.CommentOn.Equals(commentOn, StringComparison.InvariantCultureIgnoreCase)))
                .AsEnumerable()
                .OrderBy(c => c.CommentDate);

            return allComments;
        }

        public void Insert(TaskComment taskComment)
        {
            var rolloutTracking = _session.RolloutTracking.FirstOrDefault(rt => rt.ProjectId.Equals(taskComment.ProjectId)
                                                                             && rt.SiteNumber.Equals(taskComment.SiteNumber)
                                                                             && rt.TaskId.Equals(taskComment.TaskId));
            if (rolloutTracking != null)
            {
                taskComment.CompletedDate = rolloutTracking.CompletedDate;
                taskComment.ForecastDate = rolloutTracking.ForecastDate;
            }


            _session.TaskComments.Add(taskComment);
            _session.SaveChanges();
        }
    }
}
