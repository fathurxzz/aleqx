using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class Article
    {
        public Article()
        {
            this.ArticleItems = new List<ArticleItem>();
            this.ArticleLangs = new List<ArticleLang>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public bool Published { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ArticleItem> ArticleItems { get; set; }
        public virtual ICollection<ArticleLang> ArticleLangs { get; set; }
    }
}
