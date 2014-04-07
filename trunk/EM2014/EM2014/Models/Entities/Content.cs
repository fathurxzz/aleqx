using System;
using System.Collections.Generic;

namespace EM2014.Models
{
    public partial class Content
    {
        public Content()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomepage { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
