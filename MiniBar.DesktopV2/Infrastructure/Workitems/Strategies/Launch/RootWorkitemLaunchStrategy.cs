using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Utility;
using Infrastructure.Interface;

namespace Infrastructure.Workitems.Strategies.Launch
{
    internal class RootWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public RootWorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        protected override async Task Execute()
        {
            Type type = Workitem.GetType();
            if(ShouldOpenModal)
                Application.Current.Dispatcher.InvokeIfNeeded(() => ((Window)Workitem.Window).Owner = Application.Current.MainWindow);

            await RunWorkitem().ConfigureAwait(false);
            


        }
    }
}
