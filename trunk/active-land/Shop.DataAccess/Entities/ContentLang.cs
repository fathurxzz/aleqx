using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ContentLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public int LanguageId { get; set; }
        public int ContentId { get; set; }
        public string Text { get; set; }
        public virtual Content Content { get; set; }
        public virtual Language Language { get; set; }
    }
}
