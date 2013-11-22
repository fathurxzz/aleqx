using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSmallSpaceGame.Api.Contracts
{
    public enum UserError
    {
        Unknow=0,
        NotFound=1,
        Locked=2
    }


    public class UserException:Exception
    {
        private UserError _error;

        public UserException()
        {
                
        }

        public UserException(string message, UserError error)
            : base(message)
        {
            _error = error;
        }

        public UserError Error
        {
            get { return _error; }
        }
    }
}
