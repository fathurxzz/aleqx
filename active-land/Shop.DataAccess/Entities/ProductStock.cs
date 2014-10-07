using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    public partial class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string StockNumber { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; }
        public virtual Product Product { get; set; }
    }
}
