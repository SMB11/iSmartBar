using Flurl.Http;
using Infrastructure.Security;
using System;
using System.Configuration;
using System.IO;

namespace Security
{
    public abstract class RestConsumingServiceBase
    {
        private string BaseUrl;

        private string _controllerName;

        public RestConsumingServiceBase(string controllerName)
        {
            _controllerName = controllerName;
            BaseUrl = ConfigurationManager.AppSettings["ProductApiUrl"];
        }

        public RestConsumingServiceBase(string controllerName, string baseurl) : this(controllerName)
        {
            BaseUrl = baseurl;
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
