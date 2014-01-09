using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuVAPI.Contracts
{
    public class ActiveDirectoryUser 
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
