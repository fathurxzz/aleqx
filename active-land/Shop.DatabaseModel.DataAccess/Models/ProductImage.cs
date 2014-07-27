using System;
using System.Collections.Generic;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageSource { get; set; }
        public bool IsDefault { get; set; }
        public virtual Product Product { get; set; }
    }
}
