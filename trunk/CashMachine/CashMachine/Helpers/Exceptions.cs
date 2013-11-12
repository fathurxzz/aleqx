using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Helpers
{
    public class CardNotFoundException:Exception
    {
        public CardNotFoundException()
        {
                
        }    

        public CardNotFoundException(string message):base(message)
        {
            
        }
    }

    public class InvalidPinException : Exception
    {

    }
}