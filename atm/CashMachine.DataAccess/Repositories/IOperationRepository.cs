using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Presentations;

namespace CashMachine.DataAccess.Repositories
{
    public interface IOperationRepository : IRepository
    {
        BalancePresentation GetBalance(string cardNumber);
        WithdrawPresentation Withdraw(string cardNumber, decimal amount);
    }
}
