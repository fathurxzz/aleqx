using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ProductAttributeStaticValueLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductAttributeStaticValueId { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ProductAttributeStaticValue ProductAttributeStaticValue { get; set; }
    }
}
