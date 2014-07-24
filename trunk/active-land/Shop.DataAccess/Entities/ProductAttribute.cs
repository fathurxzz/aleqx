using System;
using System.Collections.Generic;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Entities
{
    public partial class ProductAttribute
    {
        public ProductAttribute()
        {
            this.ProductAttributeLangs = new List<ProductAttributeLang>();
            this.ProductAttributeStaticValues = new List<ProductAttributeStaticValue>();
            this.ProductAttributeValues = new List<ProductAttributeValue>();
            this.Categories = new List<Category>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsStatic { get; set; }
        public bool DisplayOnPreview { get; set; }
        public bool IsFilterable { get; set; }
        public virtual ICollection<ProductAttributeLang> ProductAttributeLangs { get; set; }
        public virtual ICollection<ProductAttributeStaticValue> ProductAttributeStaticValues { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
