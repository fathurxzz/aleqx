using System;

namespace CashMachine.DataAccess.Presentations
{
    public abstract class OperationResultPresentation
    {
        private string _cardNumber;
        public DateTime Date { get; set; }
        public string CardNumber
        {
            get { return _cardNumber.Insert(4, "-").Insert(9, "-").Insert(14, "-"); }
            set { _cardNumber = value; }
        }
        public decimal Balance { get; set; }
    }
}