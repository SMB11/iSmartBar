using Infrastructure.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Events
{
    public class ConnectionStateChangedEventArgs: EventArgs
    {
        public ConnectionState Value;
    }
}
