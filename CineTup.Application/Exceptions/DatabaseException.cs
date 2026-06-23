using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
