using System;

namespace AccuV.DataAccess.Entities
{
    public class TaskComment
    {
        public string TaskId { get; set; }
        public string SiteNumber { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public string CommentOn { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
