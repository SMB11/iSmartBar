using Infrastructure.Utility;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems.Strategies.Close
{
    internal abstract class WorkitemCloseStrategy
    {
        protected ContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }

        internal WorkitemCloseStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
        }

        public static WorkitemCloseStrategy GetCloseStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            if(workItem.Parent != null)
                return new ChildWorkitemCloseStrategy(currentContextService, workItem);
            else
                return new RootWorkitemCloseStrategy(currentContextService, workItem);
        }

        protected abstract Task Execute();

        public async Task Close()
        {
            if (Workitem is NullWorkitem)
                return;

            if (!Workitem.IsOpen)
            {
                if (CurrentContextService.Collection.Contains(Workitem))
                    CurrentContextService.Collection.Remove(Workitem);
                return;
            }
            Workitem.IsOpen = false;

            try
            {
                await Execute().ConfigureAwait(false);


                Workitem.Dispose();
                CommandManager.ClearWorkitemCommands(Workitem);

                Application.Current.Dispatcher.InvokeIfNeeded(() =>
                {
                    CurrentContextService.RegionManager.RemoveWorkitemViews(Workitem);
                });

                if (Workitem.IsModal)
                {

                    Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window.TryClose());
                }

                CurrentContextService.Collection.Remove(Workitem);
            }
            catch(Exception e)
            {

            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
