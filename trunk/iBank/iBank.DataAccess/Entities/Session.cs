using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBank.DataAccess.Entities
{
    public class Session
    {
        public Session()
        {
            this.Token = new List<Token>();
        }

        public int Id { get; set; }
        public string ExpireDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Token> Token { get; set; }
    }
}
