using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ProductAttributeValueTag
    {
        public ProductAttributeValueTag()
        {
            this.ProductAttributeValues = new List<ProductAttributeValue>();
            this.ProductAttributeValueTagLangs = new List<ProductAttributeValueTagLang>();
        }

        public int Id { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<ProductAttributeValueTagLang> ProductAttributeValueTagLangs { get; set; }
    }
}
