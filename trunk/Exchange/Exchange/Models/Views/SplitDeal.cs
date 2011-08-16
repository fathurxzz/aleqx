using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Views
{
    public class SplitDeal
    {
        public List<Order> MatchedOrders { get; set; }
        public Order CurrentOrder { get; set; }
        public int DealId { get; set; }
        public int OrderId { get; set; }
        public decimal OldSum { get; set; }
        public string NewOrderNumber { get; set; }
        public decimal NewOrderDealSum { get; set; }
    }
}