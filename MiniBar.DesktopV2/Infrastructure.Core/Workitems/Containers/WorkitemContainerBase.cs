using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    public abstract class WorkitemContainerBase: IDisposable
    {
        private List<IDisposable> Disposables;

        protected IWorkItem WorkItem { get; set; }

        public WorkitemContainerBase(IWorkItem workItem)
        {
            WorkItem = workItem;
            Disposables = new List<IDisposable>();
        }

        protected void Disposable(IDisposable disposable)
        {
            Disposables.Add(disposable);
        }

        public void Dispose()
        {
            Disposables.ForEach(d => d?.Dispose());
        }
    }
}
