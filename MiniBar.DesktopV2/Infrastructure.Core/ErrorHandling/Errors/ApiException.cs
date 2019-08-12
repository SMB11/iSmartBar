using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ErrorHandling
{
    public class ApiException : Exception
    {

        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiException() { }
        public ApiException(string message, HttpStatusCode httpStatusCode) : base(message) {
            HttpStatusCode = httpStatusCode;
        }
    }
}
