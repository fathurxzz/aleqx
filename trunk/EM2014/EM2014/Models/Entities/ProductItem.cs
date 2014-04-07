using System;
using System.Collections.Generic;

namespace EM2014.Models
{
    public partial class ProductItem
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
