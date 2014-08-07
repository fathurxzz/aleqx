using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ProductAttributeValueTag
    {
        public ProductAttributeValueTag()
        {
            this.ProductAttributeValues = new List<ProductAttributeValue>();
            this.ProductAttributeValueTagLangs = new List<ProductAttributeValueTagLang>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<ProductAttributeValueTagLang> ProductAttributeValueTagLangs { get; set; }
    }
}
