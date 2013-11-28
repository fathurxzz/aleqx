using System;

namespace SpaceGame.Api.Contracts.Exceptions
{
    public enum UserError
    {
        Unknow = 0,
        UserNotFound = 1,
        InvalidEmail=2,
        EmailAlreadyExists=3
    }

    public class UserException : Exception
    {
        private readonly UserError _error;

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
            get
            {
                return _error;
            }
        }
    }
}