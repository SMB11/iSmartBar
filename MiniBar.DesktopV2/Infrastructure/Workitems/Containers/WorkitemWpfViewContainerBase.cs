using Infrastructure.Interface;
using Prism.Regions;
using System;
using System.Windows;

namespace Infrastructure.Workitems
{
    abstract class WorkitemWpfViewContainerBase : WorkitemViewContainerBase
    {
        protected IRegionManager RegionManager { get; private set; }

        public WorkitemWpfViewContainerBase(IWorkItem workItem, IRegionManager regionManager) : base(workItem)
        {
            RegionManager = regionManager;
        }

        protected override void ProcessView(object view)
        {
            if (view is DependencyObject)
            {
                if (WorkitemManager.GetOwner(view as DependencyObject) != null) return;
                WorkitemManager.SetOwner(view as DependencyObject, (IWorkItem)WorkItem);
            }

            if (view is FrameworkElement)
            {
                object viewModel = ((FrameworkElement)view).DataContext;
                if (viewModel is IWorkitemAware)
                    ((IWorkitemAware)viewModel).SetWorkitem((IWorkItem)WorkItem);

                if (view is IDisposable)
                    Disposable((IDisposable)view);

                if (viewModel is IDisposable)
                    Disposable((IDisposable)viewModel);

                if (view is IGridView && viewModel is IGridViewModel)
                    ((IGridViewModel)viewModel).Grid = ((IGridView)view).Grid;
            }
        }




    }
}
