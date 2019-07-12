using Infrastructure.Interface.Enums;
using Infrastructure.Interface.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IConnectionMonitoringService
    {
        Task<ConnectionState> GetStateAsync();
        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    }
}
