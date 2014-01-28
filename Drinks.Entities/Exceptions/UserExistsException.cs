using System;

namespace Drinks.Entities.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() { }

        public UserExistsException(string duplicateFieldName)
            : base("Duplicate field: " + duplicateFieldName)
        { }
    }
}
