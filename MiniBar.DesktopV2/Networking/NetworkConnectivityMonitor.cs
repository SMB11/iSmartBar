using Networking.Events;
using NETWORKLIST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public class NetworkConnectivityMonitor : INetworkListManagerEvents
    {
        private int m_cookie = 0;
        private IConnectionPoint m_icp;
        private INetworkListManager m_nlm;

        public NetworkConnectivityMonitor()
        {
            m_nlm = new NetworkListManager();
        }

        public event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;

        void INetworkListManagerEvents.ConnectivityChanged(NLM_CONNECTIVITY newConnectivity)
        {
            NetworkStatus status = NetworkStatusParses.Parse(newConnectivity);
            NetworkStatusChanged?.Invoke(this, new NetworkStatusChangedEventArgs { Value = status });

        }

        public void Subscribe()
        {
            IConnectionPointContainer icpc = (IConnectionPointContainer)m_nlm;
            Guid tempGuid = typeof(INetworkListManagerEvents).GUID;
            icpc.FindConnectionPoint(ref tempGuid, out m_icp);
            m_icp.Advise(this, out m_cookie);
        }

        public void Unsubscribe()
        {
            m_icp.Unadvise(m_cookie);
        }
    }
}
