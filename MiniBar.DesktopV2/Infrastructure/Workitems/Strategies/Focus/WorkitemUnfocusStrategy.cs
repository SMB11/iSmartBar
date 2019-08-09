using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure.Utility;
using Infrastructure.Interface;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal class WorkitemUnfocusStrategy : WorkitemFocusStrategy
    {
        public WorkitemUnfocusStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            await TaskManager.Run(() =>
            {
                foreach (IWorkItem item in CurrentContextService.Collection.ToList())
                {
                    if (item.IsFocused != false)
                        item.IsFocused = false;
                }
                if(CurrentContextService.Collection.Null != null)
                    CurrentContextService.Collection.Null.IsFocused = true;
            });
        }
    }
}
