using NETWORKLIST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Flags]
    public enum NetworkStatus
    {
        DISCONNECTED = 0,
        IPV4_NOTRAFFIC = 1,
        IPV6_NOTRAFFIC = 2,
        IPV4_SUBNET = 16,
        IPV4_LOCALNETWORK = 32,
        IPV4_INTERNET = 64,
        IPV6_SUBNET = 256,
        IPV6_LOCALNETWORK = 512,
        IPV6_INTERNET = 1024
    }

    public static class NetworkStatusParses
    {
        public static NetworkStatus Parse(NLM_CONNECTIVITY nLM_CONNECTIVITY)
        {
            return (NetworkStatus)(int)nLM_CONNECTIVITY;
        }
    }

}
