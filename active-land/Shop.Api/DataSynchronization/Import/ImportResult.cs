using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Api.DataSynchronization.Import
{
    public class ImportResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        //public int TotalRows { get; set; }
        public int UpdatedProductCount { get; set; }
        public int ProductCount { get; set; }
        public int NewProductCount { get; set; }
        public int AddProductFailedCount { get; set; }
        public int UpdateProductFailedCount { get; set; }
        public int ReadProductFailedCount { get; set; }

        public int DeletedArticles { get; set; }
        public int AddedArticles { get; set; }
        public int UpdatedArticles { get; set; }

        public Dictionary<string, string> ProductErrors { get; set; }

    }
}
