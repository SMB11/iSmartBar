using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Extensions;
using Infrastructure.Interface;

namespace Infrastructure.Workitems.Strategies.Launch
{
    internal class ChildWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public ChildWorkitemLaunchStrategy(CurrentContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        protected override void Execute()
        {

            Type type = Workitem.GetType();
            CurrentContextService.Collection.Add(Workitem);
            IWorkItem workitem = Workitem as IWorkItem;
            Workitem.Parent = Parent;
            Workitem.Window.Owner = Parent.Window;
            Application.Current.Dispatcher.InvokeIfNeeded(() =>
            {
                BeforeWorkitemRun();
                Workitem.Run();
            });

            if (!(Workitem is IModalWorkitem))
                CurrentContextService.FocusWorkitem(Workitem);
        }

        protected virtual void BeforeWorkitemRun()
        {
        }
    }
}
