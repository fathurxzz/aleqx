using System;
using System.Collections.Generic;
using System.Linq;

namespace Leo.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Children = new List<Category>();
            this.CategoryImages = new List<CategoryImage>();
            this.CategoryLangs = new List<CategoryLang>();
            this.Products = new List<Product>();
            this.Articles = new List<Article>();

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CategoryLevel { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public bool IsNewsCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<CategoryImage> CategoryImages { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }



        public bool HasContentPages
        {
            get
            {
                return Products != null && Products.Any(p => p.IsContentPage);
            }
        }
    }
}
