using System;

namespace AccuV.DataAccess.Entities
{
    public partial class GCSite
    {
        public string SiteNumber { get; set; }
        public string ProjectId { get; set; }
        public string GcTypeId { get; set; }
        public int GcId { get; set; }
        public bool Active { get; set; }
        public DateTime LastModified { get; set; }
        public virtual GeneralContractor GeneralContractor { get; set; }
    }
}
