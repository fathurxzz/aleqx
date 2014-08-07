using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ArticleItemImage
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        public int SortOrder { get; set; }
        public int ArticleItemId { get; set; }
        public virtual ArticleItem ArticleItem { get; set; }
    }
}
