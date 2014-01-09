using System;
using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public class GeneralContractor
    {
        public GeneralContractor()
        {
            this.GcSite = new List<GCSite>();
        }

        public int GcId { get; set; }
        public string GcUsername { get; set; }
        public string GcName { get; set; }
        public string GcTypeId { get; set; }
        public bool Active { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<GCSite> GcSite { get; set; }
    }
}