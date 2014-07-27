using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ProductAttributeLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UnitTitle { get; set; }
        public int LanguageId { get; set; }
        public int ProductAttributeId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
