using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess;
using CashMachine.DataAccess.Presentations;

namespace CashMachine.DataAccess.Repositories
{
  public interface IOperationRepository : IRepository
  {
      BalancePresentation GetBalance(string cardNumber);
      WithdrawPresentation Withdraw(string cardNumber, decimal amount);
  }
}