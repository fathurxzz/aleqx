using System;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class ReportHazard
    {
        public int ReportId { get; set; }
        public int HazardId { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public virtual Report DfrNexiusFusion { get; set; }
        public virtual Hazard Hazard { get; set; }
    }
}
