using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ProductAttributeValue
    {
        public ProductAttributeValue()
        {
            this.ProductAttributeValueLangs = new List<ProductAttributeValueLang>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ProductAttributeId { get; set; }
        public Nullable<int> ProductAttributeValueTagId { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual ICollection<ProductAttributeValueLang> ProductAttributeValueLangs { get; set; }
        public virtual ProductAttributeValueTag ProductAttributeValueTag { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
