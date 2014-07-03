using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class SpecialContentLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int SpecialContentId { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual SpecialContent SpecialContent { get; set; }
    }
}
