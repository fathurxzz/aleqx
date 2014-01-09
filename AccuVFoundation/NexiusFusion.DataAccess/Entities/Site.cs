using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Site
    {
        public Site()
        {
            this.Reports = new List<Report>();
        }

        public string SiteNumber { get; set; }
        public string ProjectId { get; set; }
        public string SiteName { get; set; }
        public string SiteDesign { get; set; }
        public string SiteType { get; set; }
        public string MarketId { get; set; }
        public string ManagerId { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual Project Project { get; set; }
    }
}
