using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class Criteria
    {
        public Criteria()
        {
            this.CompanyLocalInfoes = new List<CompanyLocalInfo>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<CompanyLocalInfo> CompanyLocalInfoes { get; set; }
    }
}
