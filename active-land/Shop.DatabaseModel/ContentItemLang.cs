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
    
    public partial class ContentItemLang
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ContentItemId { get; set; }
        public int LanguageId { get; set; }
    
        public virtual ContentItem ContentItem { get; set; }
        public virtual Language Language { get; set; }
    }
}
