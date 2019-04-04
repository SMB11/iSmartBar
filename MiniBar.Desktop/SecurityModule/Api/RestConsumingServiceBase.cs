using Flurl.Http;
using Infrastructure.Security;
using System;
using System.IO;

namespace Security
{
    public abstract class RestConsumingServiceBase
    {
        public const string BaseUrl = "https://localhost:44396/api/";

        private string _controllerName;

        public RestConsumingServiceBase(string controllerName)
        {
            _controllerName = controllerName;
        }

        protected string ControllerName
        {
            get
            {
                return _controllerName;
            }
        }

        protected string ControllerPath
        {
            get
            {
                return BaseUrl + ControllerName + "/";
            }
        }

        protected IFlurlRequest BuildRequest(string url = "")
        {
            IFlurlRequest request = Path.Combine(ControllerPath, url).ConfigureRequest(_ => { });
            if (AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                request = request.WithHeader("Authorization", String.Format("Bearer {0}", AppSecurityContext.CurrentPrincipal.Identity.Token));
            return request;
        }
    }
}
