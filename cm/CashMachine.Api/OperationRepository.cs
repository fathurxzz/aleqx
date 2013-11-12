using System;
using System.Linq;
using CashMachine.DataAccess;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.Api
{
    public class OperationRepository : IOperationRepository
    {
        private ICardStore _cardStore = null;

        public OperationRepository(ICardStore cardStore)
        {
            _cardStore = cardStore;
        }

        public decimal GetBalance(string cardNumber)
        {
            var card = _cardStore.Cards.Single(c => c.Number == cardNumber);
            _cardStore.Operations.Add(new Operation
                {
                    Amount = card.Balance,
                    CardId = card.Id,
                    Date = DateTime.Now,
                    OperationType = OperationType.BalanceCheck
                });
            _cardStore.SaveChanges();
            return card.Balance;
        }

        public bool Withdraw(string cardNumber, decimal amount)
        {
            var card = _cardStore.Cards.Single(c => c.Number == cardNumber);
            if (card.Balance < amount)
            {
                throw new CardException("Insufficient funds", CardError.InsufficientFunds);
            }
            card.Balance -= amount;
            _cardStore.Operations.Add(new Operation
            {
                Amount = amount,
                CardId = card.Id,
                Date = DateTime.Now,
                OperationType = OperationType.Withdraw
            });

            _cardStore.SaveChanges();

            return true;
        }
    }
}