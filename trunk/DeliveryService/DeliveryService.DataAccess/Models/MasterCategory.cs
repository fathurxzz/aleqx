using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class MasterCategory
    {
        public MasterCategory()
        {
            this.Cities = new List<City>();
            this.Companies = new List<Company>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
