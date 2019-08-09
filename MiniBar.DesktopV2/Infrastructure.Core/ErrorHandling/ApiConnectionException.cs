using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ErrorHandling
{
    public class ApiConnectionException : Exception
    {

        public ApiConnectionException() { }
        public ApiConnectionException(string message) : base(message) { }
    }
}
