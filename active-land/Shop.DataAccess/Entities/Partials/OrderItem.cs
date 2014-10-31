using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class OrderItem
    {
        public string CategoryName { get; set; }
        public List<ProductStock> ProductStocks { get; set; }
    }
}
