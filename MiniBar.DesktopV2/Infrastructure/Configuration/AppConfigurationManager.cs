using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public static class AppConfigurationManager
    {
        public static string CheckInternetConnectionPingAddress
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CheckInternetConnectionPingAddress");
            }
        }


        public static string Title
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Title");
            }
        }
    }
}
