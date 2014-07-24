using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class Category
    {
        public Category()
        {
            this.Category1 = new List<Category>();
            this.CategoryLangs = new List<CategoryLang>();
            this.Products = new List<Product>();
            this.ProductAttributes = new List<ProductAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string CategoryLevel { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public virtual ICollection<Category> Category1 { get; set; }
        public virtual Category Category2 { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
