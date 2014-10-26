using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Api.DataSynchronization.Import;
using Shop.DataAccess.Entities;

namespace Shop.WebSite.Areas.Admin.Models
{
    public class DataTransferModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public ImportResult ImportResult { get; set; }
    }
}