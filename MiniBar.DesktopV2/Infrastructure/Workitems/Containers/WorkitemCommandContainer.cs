using Infrastructure.Interface;
using System.Windows.Input;

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
