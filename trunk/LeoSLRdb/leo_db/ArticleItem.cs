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
    
    public partial class ArticleItem
    {
        public ArticleItem()
        {
            this.ArticleItemImages = new HashSet<ArticleItemImage>();
            this.ArticleItemLangs = new HashSet<ArticleItemLang>();
        }
    
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ArticleId { get; set; }
        public int ContentType { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ICollection<ArticleItemImage> ArticleItemImages { get; set; }
        public virtual ICollection<ArticleItemLang> ArticleItemLangs { get; set; }
    }
}