using System;
using System.Collections.Generic;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Utility;

namespace Infrastructure.Workitems
{
    public abstract class WorkitemViewContainerBase : WorkitemContainerBase, IViewContainer
    {

        public WorkitemViewContainerBase(IWorkItem workItem) : base(workItem)
        {
            Views = new Dictionary<ScreenRegion, List<ViewDescriptor>>();
            Disposable(new DisposableAction(() => Views?.Clear()));
        }

        internal Dictionary<ScreenRegion, List<ViewDescriptor>> Views;

        protected virtual void ProcessView(object view)
        {
        }

        public void ImportFrom(IViewContainer container)
        {
            if(container is WorkitemViewContainerBase)
            {
                WorkitemViewContainerBase viewContainer = (WorkitemViewContainerBase)container;
                foreach (ScreenRegion region in viewContainer.Views.Keys)
                {
                    foreach (ViewDescriptor view in viewContainer.Views[region])
                    {
                        viewContainer.UnregisterView(view.Modified, region);
                        RegisterView(view.Unmodified, region);
                    }
                }

            }
               
        }
        
        public object Register(object view, ScreenRegion region)
        {
            ProcessView(view);
            object newView = RegisterView(view, region);
            ViewDescriptor descriptor = new ViewDescriptor(newView, view);
            if (Views.ContainsKey(region))
                Views[region].Add(descriptor);
            else
                Views.Add(region, new List<ViewDescriptor>() { descriptor });
            return view;
        }

        public void Unregister(object view, ScreenRegion region)
        {
            UnregisterView(view, region);
        }

        protected abstract object RegisterView(object view, ScreenRegion region);
        protected abstract void UnregisterView(object view, ScreenRegion region);



    }
}
