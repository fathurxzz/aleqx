using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class ProductLocalInfo
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual Product Product { get; set; }
    }
}
