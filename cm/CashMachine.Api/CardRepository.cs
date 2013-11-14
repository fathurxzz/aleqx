using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;
using Omu.ValueInjecter;

namespace CashMachine.Api
{
    public class CardRepository : ICardRepository
    {
        private ICardStore _store;

        public CardRepository(ICardStore store)
        {
            _store = store;
        }

        public Card GetCard(string number)
        {
            try
            {
                var card = _store.Cards.SingleOrDefault(c => c.Number == number);
                if (card == null)
                {
                    throw new CardException("Карта не найдена", CardError.NotFound);
                }
                if (card.Locked)
                {
                    throw new CardException("Карта заблокирована", CardError.Locked);
                }

                return card;
            }
            catch (InvalidOperationException ex)
            {
                throw new CardException("Repository is invalid", CardError.Unknown);
            }
        }

        public bool Validate(string number, string pin)
        {
            var card = _store.Cards.Where(c => !c.Locked).SingleOrDefault(c => c.Number == number);
            if (card == null)
                throw new CardException("Действующая карта не найдна", CardError.NotFound);
            if (card.Pin != pin)
            {
                card.PinAttemptsCount++;
                if (card.PinAttemptsCount >= 4)
                {
                    card.Locked = true;
                }
                _store.SaveChanges();

                if (card.Locked)
                    throw new CardException("Карта заблокирована", CardError.Locked);

                throw new ValidationException("Неправильный пин-код");
            }
            card.PinAttemptsCount = 0;
            _store.SaveChanges();
            return true;
        }

        public void Save(Card card)
        {
            var originalCard = _store.Cards.SingleOrDefault(c => c.Id == card.Id);
            originalCard.InjectFrom(card);
            _store.SaveChanges();
        }
    }
}
