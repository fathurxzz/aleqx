using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Helpers
{
    public enum CardError
    {
        Unknown = 0,
        NotFound = 1,
        Locked = 2,
        InsufficientFunds = 3
    }

    public class CardException:Exception
    {
        private readonly CardError _error;

        public CardException()
        {
            
        }    

        public CardException(string message, CardError error):base(message)
        {
            _error = error;
        }

        public CardError Error
        {
            get { return _error; }
        }
    }

    public class ValidationException : Exception
    {
                public ValidationException()
        {
            
        }

                public ValidationException(string message)
                    : base(message)
        {
            
        }
    }
}