using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ProductAttributeStaticValue
    {
        public ProductAttributeStaticValue()
        {
            this.ProductAttributeStaticValueLangs = new List<ProductAttributeStaticValueLang>();
        }

        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual ICollection<ProductAttributeStaticValueLang> ProductAttributeStaticValueLangs { get; set; }
    }
}
