using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security.Internal.Entities
{
    class AppPrincipal : IAppPrincipal
    {
        internal static AppPrincipal Anonymous
        {
            get
            {
                return new AppPrincipal(new AnonymousIdentity());
            }
        }

        private IAppIdentity _identity;

        public IAppIdentity Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
        public AppPrincipal(IAppIdentity identity)
        {
            _identity = identity;
        }
        
    }
}
