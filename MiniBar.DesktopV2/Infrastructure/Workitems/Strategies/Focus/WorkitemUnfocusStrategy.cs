using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Extensions;
using Infrastructure.Interface;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal class WorkitemUnfocusStrategy : WorkitemFocusStrategy
    {
        public WorkitemUnfocusStrategy(CurrentContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        public override void Focus()
        {
            base.Focus();

            foreach (IWorkItem item in CurrentContextService.Collection)
            {
                bool isFocused = false;
                if (item.IsFocused != isFocused)
                    Application.Current.Dispatcher.InvokeIfNeeded(() => item.IsFocused = isFocused);
            }
        }
    }
}
