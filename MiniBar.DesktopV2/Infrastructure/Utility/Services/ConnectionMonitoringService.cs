using Infrastructure.Interface;
using Infrastructure.Interface.Enums;
using Infrastructure.Interface.Events;
using Networking;
using NETWORKLIST;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class ConnectionMonitoringService : IConnectionMonitoringService
    {
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;

        NetworkConnectivityMonitor NetworkConnectivityMonitor;

        public ConnectionMonitoringService()
        {

            NetworkListManager nlmUser = new NetworkListManager();
            NetworkConnectivityMonitor = new NetworkConnectivityMonitor();
            NetworkConnectivityMonitor.Subscribe();
            NetworkConnectivityMonitor.NetworkStatusChanged += NetworkConnectivityMonitor_NetworkStatusChanged;
        }

        private async void NetworkConnectivityMonitor_NetworkStatusChanged(object sender, Networking.Events.NetworkStatusChangedEventArgs e)
        {
            if ((e.Value & NetworkStatus.IPV4_INTERNET) != 0 || (e.Value & NetworkStatus.IPV6_INTERNET) != 0)
            {
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs { Value = ConnectionState.Connecting });
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs { Value = await GetStateAsync() });
            }

            if (e.Value == NetworkStatus.DISCONNECTED
                && ((e.Value & NetworkStatus.IPV4_INTERNET) == 0)
                && ((e.Value & NetworkStatus.IPV6_INTERNET) == 0))
            {
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs { Value = ConnectionState.Disconnected });
            }
        }

        public async Task<ConnectionState> GetStateAsync()
        {
            if (await ConnectionManager.CheckInternetAccessAsync(ConfigurationManager.AppSettings.Get("CheckInternetConnectionPingAddress")))
                return Interface.Enums.ConnectionState.Connected;
            else
                return Interface.Enums.ConnectionState.Disconnected;
        }
    }
}
