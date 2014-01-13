using System;

namespace iBank.Api.Exceptions
{
    public enum RepositoryError
    {
        Unknow = 0
    }

    public class RepositoryException : Exception
    {
        private readonly RepositoryError _error;

        public RepositoryException()
        {

        }

        public RepositoryException(string message, RepositoryError error)
            : base(message)
        {
            _error = error;
        }

        public RepositoryError Error
        {
            get { return _error; }
        }
    }
}