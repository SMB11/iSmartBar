using DevExpress.Xpf.Core;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Prism.Regions;
using System;
using System.Windows;

namespace Infrastructure.Workitems
{
    class WorkitemViewContainer : WorkitemWpfViewContainerBase, IViewContainer, IDisposable
    {
        DynamicRegionNames RegionNames;

        internal WorkitemViewContainer(IWorkItem workItem, IRegionManager regionManager): base(workItem,regionManager)
        {
            RegionNames = RegionNameManager.GetDynamicRegionNames(Application.Current.MainWindow);
        }
        
        protected override object RegisterView(object view, ScreenRegion region)
        {
            switch (region)
            {
                case ScreenRegion.Content:
                    return RegisterInContentRegion(view);
                case ScreenRegion.Ribbon:
                    return RegisterInRibbonRegion(view);
                default:
                    throw new ArgumentException("ScreenRegion cannot be unset");
            }
        }

        protected override void UnregisterView(object view, ScreenRegion region)
        {
            switch (region)
            {
                case ScreenRegion.Content:
                    UnregisterInContentRegion(view);
                    break;
                case ScreenRegion.Ribbon:
                    UnregisterInRibbonRegion(view);
                    break;
                default:
                    throw new ArgumentException("ScreenRegion cannot be unset");
            }
        }

        private object RegisterInContentRegion(object view)
        {
            DXTabItem item;
            if (view is DXTabItem)
            {
                item = (DXTabItem)view;
            }
            else
            {
            item  = new DXTabItem() { Content = view, Header = WorkItem.WorkItemName };
            //TabControlStretchView.SetPinMode(item, TabPinMode.Left);
            if (WorkItem != null)
            {
                SetTabHeader(item, WorkItem.WorkItemName, WorkItem.IsDirty);

                WorkitemManager.SetOwner(item, (IWorkItem)WorkItem);
                WorkItem.IsDirtyChanged += (o, e) =>
                {
                    SetTabHeader(item, WorkItem.WorkItemName, WorkItem.IsDirty);
                };
                }

            }
            RegionManager.AddToRegion(RegionNames.GetName(ScreenRegion.Content), item);

            return view;
        }

        private object RegisterInRibbonRegion(object view)
        {

            RegionManager.AddToRegion(RegionNames.GetName(ScreenRegion.Ribbon), view);

            return view;
        }

        private static void SetTabHeader(DXTabItem item, string header, bool isDirty)
        {
            if (isDirty)
                item.Header = header + "*";
            else
                item.Header = header;
        }

        private void UnregisterInContentRegion(object view)
        {

            //RegionManager.Regions[Infrastructure.Constants.RegionNames.TabRegion].Remove(view);

        }

        private void UnregisterInRibbonRegion(object view)
        {
            RegionManager.Regions[RegionNames.GetName(ScreenRegion.Ribbon)].Remove(view);
        }
    }
}
