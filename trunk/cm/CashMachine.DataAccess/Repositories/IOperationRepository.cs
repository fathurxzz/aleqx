using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.Repositories
{
  public interface IOperationRepository : IRepository
  {
      decimal GetBalance(string cardNumber);
      bool Withdraw(string cardNumber, decimal amount);
  }
}