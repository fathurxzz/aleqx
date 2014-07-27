using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ProductAttributeStaticValues = new List<ProductAttributeStaticValue>();
            this.ProductImages = new List<ProductImage>();
            this.ProductLangs = new List<ProductLang>();
            this.ProductAttributeValues = new List<ProductAttributeValue>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsNew { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsTopSale { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductAttributeStaticValue> ProductAttributeStaticValues { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductLang> ProductLangs { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
