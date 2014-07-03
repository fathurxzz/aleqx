using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class Language
    {
        public Language()
        {
            this.ArticleItemLangs = new List<ArticleItemLang>();
            this.ArticleLangs = new List<ArticleLang>();
            this.CategoryLangs = new List<CategoryLang>();
            this.ProductLangs = new List<ProductLang>();
            this.SpecialContentLangs = new List<SpecialContentLang>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ArticleItemLang> ArticleItemLangs { get; set; }
        public virtual ICollection<ArticleLang> ArticleLangs { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<ProductLang> ProductLangs { get; set; }
        public virtual ICollection<SpecialContentLang> SpecialContentLangs { get; set; }
    }
}
