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
    
    public partial class Article
    {
        public Article()
        {
            this.ArticleLangs = new HashSet<ArticleLang>();
            this.ArticleItems = new HashSet<ArticleItem>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public bool Published { get; set; }
    
        public virtual ICollection<ArticleLang> ArticleLangs { get; set; }
        public virtual ICollection<ArticleItem> ArticleItems { get; set; }
    }
}