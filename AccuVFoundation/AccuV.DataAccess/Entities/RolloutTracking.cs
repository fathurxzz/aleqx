using System;

namespace AccuV.DataAccess.Entities
{
    public partial class RolloutTracking
    {
        public string SiteNumber { get; set; }
        public string TaskId { get; set; }
        public string ProjectId { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> ForecastDate { get; set; }
        public System.DateTime LastModified { get; set; }
        public virtual Site Site { get; set; }
        public virtual Task Task { get; set; }
    }
}
