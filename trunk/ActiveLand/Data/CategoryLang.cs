//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CategoryLang
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Category Category { get; set; }
    }
}
