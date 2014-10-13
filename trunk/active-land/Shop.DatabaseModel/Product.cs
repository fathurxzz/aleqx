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
    
    public partial class Product
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.ProductLangs = new HashSet<ProductLang>();
            this.ProductAttributeValues = new HashSet<ProductAttributeValue>();
            this.ProductAttributeStaticValues = new HashSet<ProductAttributeStaticValue>();
            this.ProductStocks = new HashSet<ProductStock>();
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
        public string SearchCriteria { get; set; }
        public string SearchCriteriaAttributes { get; set; }
        public string ExternalId { get; set; }
        public string OriginalUrl { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductLang> ProductLangs { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<ProductAttributeStaticValue> ProductAttributeStaticValues { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
