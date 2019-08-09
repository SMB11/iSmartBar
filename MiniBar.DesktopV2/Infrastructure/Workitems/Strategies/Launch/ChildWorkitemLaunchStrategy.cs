using System;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Interface;
using Infrastructure.Utility;

namespace Infrastructure.Workitems.Strategies.Launch
{
    internal class ChildWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public ChildWorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        protected override async Task Execute()
        {
            if (Parent.IsModal && !ShouldOpenModal)
                throw new ArgumentException("Child workitem of modal must be modal");
            Type type = Workitem.GetType();
            IWorkItem workitem = Workitem as IWorkItem;
            Workitem.Parent = Parent;
            if (ShouldOpenModal)
                Application.Current.Dispatcher.InvokeIfNeeded(() => ((Window) Workitem.Window).Owner = ((Window)Parent.Window) ?? Application.Current.MainWindow);

            BeforeWorkitemRun();
            await RunWorkitem().ConfigureAwait(false);
            
        }

        protected virtual void BeforeWorkitemRun()
        {
        }
    }
}
