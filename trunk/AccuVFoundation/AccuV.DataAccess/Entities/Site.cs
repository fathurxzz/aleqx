using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Site
    {
        public Site()
        {
            RolloutTracking = new List<RolloutTracking>();
        }

        public string SiteNumber { get; set; }
        public string ProjectId { get; set; }
        public string SiteName { get; set; }
        public string SiteDesign { get; set; }
        public string SiteType { get; set; }
        public string MarketId { get; set; }
        public string ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Market Market { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<RolloutTracking> RolloutTracking { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ActivityType> ActivityTypes { get; set; }
    }
}
