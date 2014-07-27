using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class CategoryLang
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
    }
}
