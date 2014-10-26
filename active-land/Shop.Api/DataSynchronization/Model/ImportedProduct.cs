using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.Api.DataSynchronization.Model
{
    public class ImportedProduct:Product
    {
        public List<ImportedProductStock> ImportedProductStocks { get; set; }
        public Dictionary<string, string> ImportedProductAttibutes { get; set; }
    }
}
