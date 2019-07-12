using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal abstract class WorkitemFocusStrategy
    {
        protected CurrentContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }

        internal WorkitemFocusStrategy(CurrentContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
        }

        public static WorkitemFocusStrategy GetFocusStrategy(CurrentContextService currentContextService, IWorkItem workItem)
        {
            if(workItem == null)
                return new WorkitemUnfocusStrategy(currentContextService, workItem);
            else if (workItem is IModalWorkitem)
                return new ModalWorkitemFocusStrategy(currentContextService, workItem);
            else if (workItem.Parent != null)
                return new ChildWorkitemFocusStrategy(currentContextService, workItem);
            else
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
        }

        public virtual void Focus()
        {
        }
    }
}
