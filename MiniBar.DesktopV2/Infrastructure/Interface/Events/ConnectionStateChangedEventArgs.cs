using Infrastructure.Interface.Enums;
using System;

namespace Infrastructure.Interface.Events
{
    public class ConnectionStateChangedEventArgs : EventArgs
    {
        public ConnectionState Value;
    }
}
