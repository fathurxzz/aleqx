//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductAttribute
    {
        public ProductAttribute()
        {
            this.Categories = new HashSet<Category>();
            this.ProductAttributeValues = new HashSet<ProductAttributeValue>();
            this.ProductAttributeStaticValues = new HashSet<ProductAttributeStaticValue>();
            this.ProductAttributeLangs = new HashSet<ProductAttributeLang>();
        }
    
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsStatic { get; set; }
        public bool DisplayOnPreview { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsPublic { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<ProductAttributeStaticValue> ProductAttributeStaticValues { get; set; }
        public virtual ICollection<ProductAttributeLang> ProductAttributeLangs { get; set; }
    }
}
