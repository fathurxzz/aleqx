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
    
    public partial class ContentLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }
        public int ContentId { get; set; }
        public string Text { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Content Content { get; set; }
    }
}
