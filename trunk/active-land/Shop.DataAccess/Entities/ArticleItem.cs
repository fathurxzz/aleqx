using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ArticleItem
    {
        public ArticleItem()
        {
            this.ArticleItemImages = new List<ArticleItemImage>();
            this.ArticleItemLangs = new List<ArticleItemLang>();
        }

        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<ArticleItemImage> ArticleItemImages { get; set; }
        public virtual ICollection<ArticleItemLang> ArticleItemLangs { get; set; }
    }
}
