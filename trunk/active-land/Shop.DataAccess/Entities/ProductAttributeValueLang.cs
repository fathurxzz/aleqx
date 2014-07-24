using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Entities
{
    public partial class ProductAttributeValueLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductAttributeValueId { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ProductAttributeValue ProductAttributeValue { get; set; }
    }
}
