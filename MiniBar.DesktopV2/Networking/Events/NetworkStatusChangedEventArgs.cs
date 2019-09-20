using System;

namespace Networking.Events
{
    public class NetworkStatusChangedEventArgs : EventArgs
    {
        public NetworkStatus Value { get; set; }
    }
}
