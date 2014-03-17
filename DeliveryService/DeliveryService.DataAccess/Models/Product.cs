using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductLocalInfoes = new List<ProductLocalInfo>();
            this.Categories = new List<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ProductLocalInfo> ProductLocalInfoes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
