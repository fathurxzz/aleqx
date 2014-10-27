using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Api.DataSynchronization.Model
{
    public class ImportedProductAttribute
    {
        public string ExternalId { get; set; }
        public bool Imported { get; set; }
        public List<string> Values { get; set; }
    }
}
