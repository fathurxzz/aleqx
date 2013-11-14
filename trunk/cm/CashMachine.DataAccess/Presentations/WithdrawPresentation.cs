using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachine.DataAccess.Presentations
{
    public class WithdrawPresentation:OperationResultPresentation
    {
        public decimal Amount { get; set; }
    }
}
