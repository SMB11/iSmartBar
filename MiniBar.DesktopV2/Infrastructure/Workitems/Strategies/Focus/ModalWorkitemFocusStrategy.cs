using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems.Strategies.Focus
{
    internal class ModalWorkitemFocusStrategy : WorkitemFocusStrategy
    {
        public ModalWorkitemFocusStrategy(CurrentContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        public override void Focus()
        {
            base.Focus();

            Workitem.IsFocused = true;
        }
    }
}
