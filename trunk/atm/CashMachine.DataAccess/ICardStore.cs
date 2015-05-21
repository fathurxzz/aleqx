using System.Data.Entity;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess
{
    public interface ICardStore
    {
        IDbSet<Card> Cards { get; }
        IDbSet<Operation> Operations { get; }
        int SaveChanges();
    }
}