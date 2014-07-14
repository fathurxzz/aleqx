using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductImages = new List<ProductImage>();
            this.ProductLangs = new List<ProductLang>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int CategoryId { get; set; }
        public bool IsContentPage { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductLang> ProductLangs { get; set; }

    }
}
