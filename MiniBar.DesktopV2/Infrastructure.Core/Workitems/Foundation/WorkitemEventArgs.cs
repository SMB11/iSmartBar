using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    /// <summary>
    /// Workitem event args to send data to its creator through the communication channel
    /// </summary>
    public class WorkitemEventArgs
    {
        public IWorkItem Sender { get; private set; }
        public object Data { get; private set; }

        public WorkitemEventArgs(IWorkItem sender, object data)
        {
            Sender = sender;
            Data = data;
        }
    }
}
