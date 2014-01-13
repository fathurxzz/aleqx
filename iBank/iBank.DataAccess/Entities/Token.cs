using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBank.DataAccess.Entities
{
    public class Token
    {
        public Token()
        {
            this.OtpSms = new HashSet<OtpSms>();
        }

        public int Id { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public string Ip { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
        public int TokenTypeId { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }

        public virtual ICollection<OtpSms> OtpSms { get; set; }
        public virtual Session Session { get; set; }
        public virtual TokenType TokenType { get; set; }
        public virtual User User { get; set; }
    }
}
