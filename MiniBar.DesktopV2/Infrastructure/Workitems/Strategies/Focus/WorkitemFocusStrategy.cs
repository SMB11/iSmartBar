using Infrastructure.Interface;
using Infrastructure.Logging;
using Infrastructure.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal abstract class WorkitemFocusStrategy
    {
        protected ContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }
        protected ICompositeLogger Logger => CommonServiceLocator.ServiceLocator.Current.GetInstance<ICompositeLogger>();
        protected ITaskManager TaskManager => CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();

        internal WorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
        }

        public static WorkitemFocusStrategy GetFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            if(workItem == null || workItem is NullWorkitem)
                return new WorkitemUnfocusStrategy(currentContextService, workItem);
            else if (workItem.IsModal)
                return new ModalWorkitemFocusStrategy(currentContextService, workItem);
            else if (workItem.Parent != null)
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
            else
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
        }

        public async Task Focus()
        {
            try
            {
                await Execute().ConfigureAwait(false);
            }
            catch(Exception e)
            {

            }
        }

        protected abstract Task Execute();
    }
}
