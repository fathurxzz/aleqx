using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class Article
    {
        public Article()
        {
            this.ArticleItems = new List<ArticleItem>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ArticleItem> ArticleItems { get; set; }
    }
}
