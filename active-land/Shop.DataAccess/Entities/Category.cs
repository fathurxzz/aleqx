using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class Category
    {
        public Category()
        {
            this.Children = new List<Category>();
            this.CategoryLangs = new List<CategoryLang>();
            this.Products = new List<Product>();
            this.ProductAttributes = new List<ProductAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CategoryLevel { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
