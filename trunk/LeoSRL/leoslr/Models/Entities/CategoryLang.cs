using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class CategoryLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
    }
}
