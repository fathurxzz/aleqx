using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class MessageService : IMessageService
    {
        public string GetWelcomeMessage()
        {
            return "Welcome to Ninject MVC4 Example1111";
        }
    }
}