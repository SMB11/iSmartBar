using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ErrorHandling
{
    public class ApiError
    {
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public string TraceID { get; set; }

    }
}
