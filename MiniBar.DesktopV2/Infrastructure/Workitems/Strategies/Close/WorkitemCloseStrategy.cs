using Infrastructure.Extensions;
using Infrastructure.Interface;
using Infrastructure.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems.Strategies.Close
{
    internal abstract class WorkitemCloseStrategy
    {
        protected CurrentContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }

        internal WorkitemCloseStrategy(CurrentContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
        }

        public static WorkitemCloseStrategy GetCloseStrategy(CurrentContextService currentContextService, IWorkItem workItem)
        {
            if(workItem.Parent != null)
                return new ChildWorkitemCloseStrategy(currentContextService, workItem);
            else
                return new RootWorkitemCloseStrategy(currentContextService, workItem);
        }

        protected abstract void Execute();

        public void Close()
        {
            if (!Workitem.IsOpen)
            {
                if (CurrentContextService.Collection.Contains(Workitem))
                    CurrentContextService.Collection.Remove(Workitem);
                return;
            }
            Workitem.IsOpen = false;

            Execute();

            CurrentContextService.Collection.Remove(Workitem);

            Application.Current.Dispatcher.InvokeIfNeeded(() =>
            {
                Workitem?.Cleanup();
                CommandManager.ClearWorkitemCommands(Workitem);
                CurrentContextService.IRegionManager.RemoveWorkitemViews(Workitem);
            });

            if (Workitem is IModalWorkitem)
            {
                Workitem.Window.TryClose();
            }
        }
    }
}
