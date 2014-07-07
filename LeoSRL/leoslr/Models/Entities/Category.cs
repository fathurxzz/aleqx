using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Children = new List<Category>();
            this.CategoryImages = new List<CategoryImage>();
            this.CategoryLangs = new List<CategoryLang>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CategoryLevel { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<CategoryImage> CategoryImages { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }

    }
}
