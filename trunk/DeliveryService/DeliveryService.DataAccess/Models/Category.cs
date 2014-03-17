using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Companies = new List<Company>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
