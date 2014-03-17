using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class Company
    {
        public Company()
        {
            this.CompanyLocalInfoes = new List<CompanyLocalInfo>();
            this.Products = new List<Product>();
            this.Cities = new List<City>();
            this.Categories = new List<Category>();
            this.MasterCategories = new List<MasterCategory>();
            this.Qualities = new List<Quality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public virtual ICollection<CompanyLocalInfo> CompanyLocalInfoes { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<MasterCategory> MasterCategories { get; set; }
        public virtual ICollection<Quality> Qualities { get; set; }
    }
}
