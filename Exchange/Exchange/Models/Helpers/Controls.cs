using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exchange.Models.Helpers
{
    public class OfficeSelectListItem : SelectListItem
    {
        public int OfficeLevel { get; set; }
    }

    public class ClientsSelectListItem : SelectListItem
    {
        public int SubjId { get; set; }
        public int CntrCode { get; set; }
        public string Okpo { get; set; }
    }
}