using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.Repositories
{
    public interface ICardRepository : IRepository
    {
        Card GetCard(string number);
        bool Validate(string number, string pin);
        void Save(Card card);
    }
}