using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.Repositories
{
    public interface ICardRepository : IRepository
    {
        Card Get(string number);
        bool Validate(string number, string pin);
        void Save(Card card);
    }
}