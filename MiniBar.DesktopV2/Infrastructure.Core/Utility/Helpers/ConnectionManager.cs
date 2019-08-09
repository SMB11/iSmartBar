using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public static class ConnectionManager
    {
        public static async Task<bool> CheckInternetAccessAsync(string host)
        {
            try
            {
                Ping myPing = new Ping();
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
    }
}
