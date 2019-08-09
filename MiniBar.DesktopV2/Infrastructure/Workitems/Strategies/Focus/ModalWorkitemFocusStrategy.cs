using Infrastructure.Utility;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal class ModalWorkitemFocusStrategy : WorkitemFocusStrategy
    {
        public ModalWorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            await TaskManager.Run(() =>
            {
                Workitem.IsFocused = true;
            }).ConfigureAwait(false);
        }
    }
}
