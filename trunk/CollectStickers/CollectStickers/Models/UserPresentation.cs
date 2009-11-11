using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollectStickers.Models
{
    public class UserPresentation
    {
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Icq { get; set; }
        public string UserId{ get; set; }
    }
}
