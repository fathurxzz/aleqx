using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ProductAttributeValueTagLang
    {
        public int Id { get; set; }
        public int ProductAttributeValueTagId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public virtual Language Language { get; set; }
        public virtual ProductAttributeValueTag ProductAttributeValueTag { get; set; }
    }
}
