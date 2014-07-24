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
    
    public partial class ProductLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public int ProductId { get; set; }
        public int LanguageId { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Product Product { get; set; }
    }
}
