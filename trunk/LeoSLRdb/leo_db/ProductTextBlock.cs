//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace leo_db
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTextBlock
    {
        public ProductTextBlock()
        {
            this.ProductTextBlockLangs = new HashSet<ProductTextBlockLang>();
            this.ProductTextBlockFiles = new HashSet<ProductTextBlockFile>();
        }
    
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ProductId { get; set; }
    
        public virtual ICollection<ProductTextBlockLang> ProductTextBlockLangs { get; set; }
        public virtual ICollection<ProductTextBlockFile> ProductTextBlockFiles { get; set; }
        public virtual Product Product { get; set; }
    }
}