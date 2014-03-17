using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class City
    {
        public City()
        {
            this.CompanyLocalInfoes = new List<CompanyLocalInfo>();
            this.ProductLocalInfoes = new List<ProductLocalInfo>();
            this.Companies = new List<Company>();
            this.MasterCategories = new List<MasterCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CompanyLocalInfo> CompanyLocalInfoes { get; set; }
        public virtual ICollection<ProductLocalInfo> ProductLocalInfoes { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<MasterCategory> MasterCategories { get; set; }
    }
}
