using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using CashMachine.Helpers;

namespace CashMachine.Models
{
    public class CardStorage : ICardStorage
    {
        private SiteContext _context;

        public CardStorage(SiteContext context)
        {
            _context = context;
        }

        public Card GetCard(string number)
        {
            return _context.Card.FirstOrDefault(c => c.Number == number);
        }

        public Card GetCard(int id)
        {
            var card = _context.Card.FirstOrDefault(c => c.Id == id);
            if (card == null)
                throw new CardNotFoundException();
            return card;
        }



        public void AddOperation(Card card, Operation operation)
        {
            throw new NotImplementedException();
        }


        public bool ValidatePin(int cardId, string cardPin, out Card card)
        {
            card = _context.Card.First(c => c.Id == cardId);
            if (card.Pin != cardPin)
            {
                card.PinAttemptsCount++;
                if (card.PinAttemptsCount >= 3)
                {
                    card.Locked = true;
                }
                _context.SaveChanges();
                return false;
            }

            if (card.PinAttemptsCount > 0)
            {
                card.PinAttemptsCount = 0;
                _context.SaveChanges();
            }
            return true;
        }

        public decimal GetBalance(int cardId)
        {
            
        }
    }
}