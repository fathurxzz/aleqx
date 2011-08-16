using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class UserMessage:CustomMessage
    {
        
        public string OfficeName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}