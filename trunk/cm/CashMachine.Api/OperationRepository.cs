using System;
using System.Linq;
using CashMachine.DataAccess;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Presentations;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.Api
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ICardStore _cardStore;

        public OperationRepository(ICardStore cardStore)
        {
            _cardStore = cardStore;
        }

        public BalancePresentation GetBalance(string cardNumber)
        {
            var date = DateTime.Now;
            var card = _cardStore.Cards.Single(c => c.Number == cardNumber);
            _cardStore.Operations.Add(new Operation
                {
                    Amount = card.Balance,
                    CardId = card.Id,
                    Date = date,
                    OperationType = OperationType.BalanceCheck
                });
            _cardStore.SaveChanges();
            return new BalancePresentation { Balance = card.Balance, CardNumber = cardNumber, Date = date };
        }

        public WithdrawPresentation Withdraw(string cardNumber, decimal amount)
        {
            var date = DateTime.Now;
            var card = _cardStore.Cards.Single(c => c.Number == cardNumber);
            if (amount <= 0)
            {
                throw new CardException("Введите сумму больше 0", CardError.WrongAmount);
            }
            if (card.Balance < amount)
            {
                throw new CardException("Сумма, которую выхотите снять больше суммы остатка карты", CardError.InsufficientFunds);
            }
            card.Balance -= amount;
            _cardStore.Operations.Add(new Operation
            {
                Amount = amount,
                CardId = card.Id,
                Date = date,
                OperationType = OperationType.Withdraw
            });

            _cardStore.SaveChanges();

            return new WithdrawPresentation
                   {
                       Amount = amount,
                       Balance = card.Balance,
                       CardNumber = card.Number,
                       Date = date
                   };
        }
    }
}