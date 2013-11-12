using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    public interface ICardStorage
    {
        Card GetCard(string number);
        Card GetCard(int id);
        void AddOperation(Card card, Operation operation);
        bool ValidatePin(int cardId, string cardPin, out Card card);
    }
}