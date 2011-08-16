using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public enum RecipientType
    {
        User = 1,
        Office = 2
    }

    public class Recepient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficeName { get; set; }
        public RecipientType Type { get; set; }
    }
}