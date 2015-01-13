using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Api.DataSynchronization.Model
{
    public class ImportedProductStock
    {
        public string StockNumber { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public bool IsDiscount { get; set; }
        public bool Imported { get; set; }
    }
}
