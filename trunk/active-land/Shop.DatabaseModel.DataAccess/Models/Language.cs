using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class Language
    {
        public Language()
        {
            this.ArticleItemLangs = new List<ArticleItemLang>();
            this.ArticleLangs = new List<ArticleLang>();
            this.CategoryLangs = new List<CategoryLang>();
            this.ContentLangs = new List<ContentLang>();
            this.ProductAttributeLangs = new List<ProductAttributeLang>();
            this.ProductAttributeStaticValueLangs = new List<ProductAttributeStaticValueLang>();
            this.ProductAttributeValueLangs = new List<ProductAttributeValueLang>();
            this.ProductAttributeValueTagLangs = new List<ProductAttributeValueTagLang>();
            this.ProductLangs = new List<ProductLang>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ArticleItemLang> ArticleItemLangs { get; set; }
        public virtual ICollection<ArticleLang> ArticleLangs { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<ContentLang> ContentLangs { get; set; }
        public virtual ICollection<ProductAttributeLang> ProductAttributeLangs { get; set; }
        public virtual ICollection<ProductAttributeStaticValueLang> ProductAttributeStaticValueLangs { get; set; }
        public virtual ICollection<ProductAttributeValueLang> ProductAttributeValueLangs { get; set; }
        public virtual ICollection<ProductAttributeValueTagLang> ProductAttributeValueTagLangs { get; set; }
        public virtual ICollection<ProductLang> ProductLangs { get; set; }
    }
}
