using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class CompanyLocalInfo
    {
        public CompanyLocalInfo()
        {
            this.Criteria = new List<Criteria>();
        }

        public int Id { get; set; }
        public string WorkTime { get; set; }
        public int CompanyId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public decimal DeliveryCost { get; set; }
        public string MinOrderAmount { get; set; }
        public string Title { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Criteria> Criteria { get; set; }
    }
}
