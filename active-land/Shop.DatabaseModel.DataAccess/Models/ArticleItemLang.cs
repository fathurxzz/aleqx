using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ArticleItemLang
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ArticleItemId { get; set; }
        public int LanguageId { get; set; }
        public virtual ArticleItem ArticleItem { get; set; }
        public virtual Language Language { get; set; }
    }
}
