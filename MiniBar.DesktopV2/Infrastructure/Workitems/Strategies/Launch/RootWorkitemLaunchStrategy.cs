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
    internal class RootWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public RootWorkitemLaunchStrategy(CurrentContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        protected override void Execute()
        {
            Type type = Workitem.GetType();
            CurrentContextService.Collection.Add(Workitem);
            if(Workitem is IModalWorkitem)
                Workitem.Window.Owner = Application.Current.MainWindow;
            Application.Current.Dispatcher.InvokeIfNeeded(() =>
            {
                Workitem.Run();
            });

            if (!(Workitem is IModalWorkitem))
                CurrentContextService.FocusWorkitem(Workitem);

        }
    }
}
