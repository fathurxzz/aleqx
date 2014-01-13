using System;

namespace iBank.Api.Exceptions
{
    public enum SecurityError
    {
        Unknow = 0,
        UserNotFound = 1,
        IncorrectLoginOrPassword = 2
    }

    public class SecurityException : Exception
    {
        private readonly SecurityError _error;

        public SecurityException()
        {

        }

        public SecurityException(string message, SecurityError error)
            : base(message)
        {
            _error = error;
        }

        public SecurityError Error
        {
            get
            {
                return _error;
            }
        }
    }
}