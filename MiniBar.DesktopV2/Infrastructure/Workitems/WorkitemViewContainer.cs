using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems
{
    public class WorkitemViewContainer : WorkitemContainerBase, IViewContainer, IDisposable
    {

        internal WorkitemViewContainer(IWorkItem workItem): base(workItem)
        {
        }

        public TView Register<TView>(object view)
        {
            return (TView)Register(view);
        }


        public object Register(object view)
        {
            if (view is DependencyObject)
                WorkitemManager.SetOwner(view as DependencyObject, WorkItem);

            if (view is FrameworkElement)
            {
                object viewModel = ((FrameworkElement)view).DataContext;
                if (viewModel is IWorkitemAware)
                    ((IWorkitemAware)viewModel).SetWorkitem(WorkItem);

                if (view is IGridView && viewModel is IGridViewModel)
                    ((IGridViewModel)viewModel).Grid = ((IGridView)view).Grid;

                if (view is IDisposable)
                    Disposable((IDisposable)view);

                if (viewModel is IDisposable)
                    Disposable((IDisposable)viewModel);
            }
            return view;
        }
    }
}
