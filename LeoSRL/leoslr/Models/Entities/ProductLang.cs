using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class ProductLang
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public virtual Language Language { get; set; }
        public virtual Product Product { get; set; }
    }
}
