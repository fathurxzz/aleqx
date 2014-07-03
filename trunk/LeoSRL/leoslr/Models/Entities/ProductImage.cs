using System;
using System.Collections.Generic;

namespace Leo.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
