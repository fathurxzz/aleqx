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
    
    public partial class Content
    {
        public Content()
        {
            this.ContentLangs = new HashSet<ContentLang>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCatalogue { get; set; }
    
        public virtual ICollection<ContentLang> ContentLangs { get; set; }
    }
}
