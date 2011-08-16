using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public class FilterValuesUpdater
    {
        public int? Status { get; set; }
        public int? TransactionStatus { get; set; }
        public string Date { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Podrid { get; set; }
        public int? CurId { get; set; }
        public int? OperId { get; set; }
        public string Course { get; set; }
        public int? OperSign { get; set; }
        public string UserName { get; set; }
    }
}