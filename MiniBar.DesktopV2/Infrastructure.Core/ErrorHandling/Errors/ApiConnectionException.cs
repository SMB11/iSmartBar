using System;

namespace Infrastructure.ErrorHandling
{
    public class ApiConnectionException : Exception
    {

        public ApiConnectionException() { }
        public ApiConnectionException(string message) : base(message) { }
    }
}
