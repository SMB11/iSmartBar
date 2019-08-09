using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure.Interface;

namespace Infrastructure.Workitems
{
    public class WorkitemCommandContainer : WorkitemContainerBase, ICommandContainer
    {
        public WorkitemCommandContainer(IWorkItem workItem) : base(workItem)
        {
        }

        public void Register(string name, ICommand command)
        {
            Disposable(CommandManager.RegisterWorkitemCommand(name, WorkItem, command));
        }
    }
}
