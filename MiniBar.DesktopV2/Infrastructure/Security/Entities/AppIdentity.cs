using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Entities
{
    public class AppIdentity
    {
        public AppIdentity(string name, string token)
        {
            Name = name;
            Token = token;
        }

        public string Name { get; private set; }

        public string AuthenticationType => "Admin";

        public bool IsAuthenticated => !String.IsNullOrEmpty(Name);

        public string Token { get; private set; }
    }
}
