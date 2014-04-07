using System;
using System.Collections.Generic;

namespace EM2014.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductItems = new List<ProductItem>();
        }

        public int Id { get; set; }
        public string ImageSource { get; set; }
        public int SortOrder { get; set; }
        public int ContentId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public virtual Content Content { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
