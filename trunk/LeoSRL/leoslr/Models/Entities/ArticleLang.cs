using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class ArticleLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArticleId { get; set; }
        public int LanguageId { get; set; }
        public virtual Article Article { get; set; }
        public virtual Language Language { get; set; }
    }
}
