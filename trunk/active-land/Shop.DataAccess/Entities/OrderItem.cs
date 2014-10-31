using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductStockNumber { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public string ProductTitle { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
