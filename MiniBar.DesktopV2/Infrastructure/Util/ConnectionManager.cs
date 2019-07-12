using Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Util
{
    public static class ConnectionManager
    {
        public static async Task<bool> CheckInternetAccessAsync()
        {
            try
            {
                Ping myPing = new Ping();
                String host = AppConfigurationManager.CheckInternetConnectionPingAddress;
                byte[] buffer = new byte[32];
                int timeout = 2000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = await myPing.SendPingAsync(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static bool CheckInternetAccess()
        {
            try
            {
                Ping myPing = new Ping();
                String host = AppConfigurationManager.CheckInternetConnectionPingAddress;
                byte[] buffer = new byte[32];
                int timeout = 2000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
