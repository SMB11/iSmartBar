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
    internal class ChildWorkitemCloseStrategy : WorkitemCloseStrategy
    {
        internal ChildWorkitemCloseStrategy(CurrentContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override void Execute()
        {

            if (!(Workitem is IModalWorkitem))
            {
                CurrentContextService.FocusWorkitem(Workitem.Parent);
            }
        }
    }
}
