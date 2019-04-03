using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public interface IAppIdentity
    {

        string Name { get; }

        string Token { get; }

        string AuthenticationType { get; }

        bool IsAuthenticated { get; }
    }
}
