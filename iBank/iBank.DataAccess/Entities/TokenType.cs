using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBank.DataAccess.Entities
{
    public class TokenType
    {
        public TokenType()
        {
            this.Token = new List<Token>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Token> Token { get; set; }
    }
}
