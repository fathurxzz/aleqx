using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public class ProductTextBlockLang
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int ProductTextBlockId { get; set; }
        public int LanguageId { get; set; }
        public virtual ProductTextBlock ProductTextBlock { get; set; }
        public virtual Language Language { get; set; }
    }
}