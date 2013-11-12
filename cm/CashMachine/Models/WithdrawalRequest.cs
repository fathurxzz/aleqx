using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    public class WithdrawalRequest
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}