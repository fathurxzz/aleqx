using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Hazard
    {
        public Hazard()
        {
            this.ReportHazards = new List<ReportHazard>();
        }

        public int HazardId { get; set; }
        public string Type { get; set; }
        public string HazardName { get; set; }
        public virtual ICollection<ReportHazard> ReportHazards { get; set; }
    }
}
