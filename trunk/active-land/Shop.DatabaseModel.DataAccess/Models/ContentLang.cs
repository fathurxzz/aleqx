using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ContentLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }
        public int ContentId { get; set; }
        public string Text { get; set; }
        public virtual Content Content { get; set; }
        public virtual Language Language { get; set; }
    }
}
