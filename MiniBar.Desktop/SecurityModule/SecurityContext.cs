using Security.Internal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public static class AppSecurityContext
    {
        private static IAppPrincipal currentPrincipal;

        public static IAppPrincipal CurrentPrincipal
        {
            get
            {
                return currentPrincipal ?? AppPrincipal.Anonymous;
            }
        }

        public static void SetCurrentPrincipal(IAppPrincipal principal) {
            currentPrincipal = principal;
        }
    }
}
