//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewVision.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthorLang
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Author Author { get; set; }
    }
}
