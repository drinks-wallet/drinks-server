using System;

namespace Drinks.Entities.Exceptions
{
    public class InvalidUserCredentialsException : Exception
    {
        public InvalidUserCredentialsException() { }

        public InvalidUserCredentialsException(Exception innerException)
            :base(string.Empty, innerException)
        { }
    }
}
