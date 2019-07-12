using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Extensions;
using Infrastructure.Interface;
using Infrastructure.Prism;

namespace Infrastructure.Workitems.Strategies.Close
{
    internal class RootWorkitemCloseStrategy : WorkitemCloseStrategy
    {
        public RootWorkitemCloseStrategy(CurrentContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override void Execute()
        {
            if(!(Workitem is IModalWorkitem))
            {
                IWorkItem toFocus = null;

                if (Workitem.IsFocused && CurrentContextService.Collection.Count > 1)
                {
                    int index = CurrentContextService.Collection.IndexOf(Workitem);
                    if (index > 0)
                        toFocus = CurrentContextService.Collection[index - 1];
                    else if (index == 0)
                        toFocus = CurrentContextService.Collection[index + 1];
                }

                if (toFocus != null)
                    CurrentContextService.FocusWorkitem(toFocus);
            }
        }
    }
}
