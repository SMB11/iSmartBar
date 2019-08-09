using Infrastructure.Interface;
using Infrastructure.Modularity;
using Prism.Regions;
using System;
using System.Windows;

namespace Infrastructure.Workitems
{
    class ModalWorkitemViewContainer : WorkitemWpfViewContainerBase, IViewContainer, IDisposable
    {
        DynamicRegionNames RegionNames;

        internal ModalWorkitemViewContainer(IWorkItem workItem, IRegionManager regionManager) : base(workItem, regionManager)
        {
            RegionNames = RegionNameManager.GetDynamicRegionNames((Window)WorkItem.Window);
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

        private object RegisterInContentRegion(object view)
        {
            RegionManager.AddToRegion(RegionNames.GetName(ScreenRegion.Content), view);

            return view;
        }

        private object RegisterInRibbonRegion(object view)
        {

            RegionManager.AddToRegion(RegionNames.GetName(ScreenRegion.Ribbon), view);
            RegionManager.Regions[RegionNames.GetName(ScreenRegion.Ribbon)].Activate(view);
            return view;
        }

        private void UnregisterInContentRegion(object view)
        {

            RegionManager.Regions[RegionNames.GetName(ScreenRegion.Content)].Remove(view);

        }

        private void UnregisterInRibbonRegion(object view)
        {
            RegionManager.Regions[RegionNames.GetName(ScreenRegion.Ribbon)].Remove(view);
        }

    }
}
