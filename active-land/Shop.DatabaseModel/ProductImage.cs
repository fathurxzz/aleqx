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
    
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageSource { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
