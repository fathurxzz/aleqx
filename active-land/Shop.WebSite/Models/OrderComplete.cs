using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Models
{
    public class OrderComplete
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
    }
}