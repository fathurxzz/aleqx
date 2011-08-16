using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class CustomMessage
    {
        public MessageType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public static partial class Extensions
    {
        public static MessageType GetMessageType(this IEnumerable<CustomMessage> list)
        {
            if (list != null)
                return list.First().Type;
            throw new ArgumentNullException();
        }

    }
    
}