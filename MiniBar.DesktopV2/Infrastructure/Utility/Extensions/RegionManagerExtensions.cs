using DevExpress.Xpf.Core;
using Infrastructure.Framework;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Prism.Regions;
using System.Linq;
using System.Windows;

namespace Infrastructure.Utility
{
    public static class RegionManagerExtensions
    {

        public static void RemoveWorkitemViews(this IRegionManager regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions)
            {
                regionManager.RemoveWorkitemFromRegion(region, workItem);
            }
        }
        public static void DeactivateWorkitem(this IRegionManager regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions.ToList())
            {
                Application.Current.Dispatcher.InvokeIfNeeded(() => regionManager.DeactivateWorkitemInRegion(region, workItem));
            }
        }

        public static void ActivateWorkitem(this IRegionManager regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions.ToList())
            {
                regionManager.ActivateWorkitemInRegion(region, workItem);
            }
        }

        public static void RemoveWorkitemFromRegion(this IRegionManager regionManager, IRegion region, IWorkItem workItem)
        {
            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {
                IWorkItem owner = WorkitemManager.GetOwner(view);
                if (workItem.Equals(owner))
                    region.Remove(view);
            }
        }

        public static void DeactivateWorkitemInRegion(this IRegionManager regionManager, IRegion region, IWorkItem workItem)
        {

            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {
                IWorkItem owner = WorkitemManager.GetOwner(view);
                if (workItem.Equals(owner))
                    region.Deactivate(view);
            }
        }

        public static void ActivateWorkitemInRegion(this IRegionManager regionManager, IRegion region, IWorkItem workItem)
        {
            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {

                CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>()
                    .RunUIThread(() =>
                    {
                        IWorkItem owner = WorkitemManager.GetOwner(view);
                        if (workItem.Equals(owner) && region.Views.Contains(view))
                            region.Activate(view);
                    });
            }
        }

        public static void ClearNavigation(this IRegionManager regionManager, string region)
        {
            regionManager.Regions[region].RemoveAll();
            regionManager.Regions[region].NavigationService.Journal.Clear();
        }


        public static void AddToolbar(this IRegionManager regionManager, BarLinkHolder toolbar, IWorkItem owner)
        {
            WorkitemManager.SetOwner(toolbar, owner);
            regionManager.AddToRegion(Constants.RegionNames.RibbonRegion, toolbar);

        }

        public static DXTabItem AddTab(this IRegionManager regionManager, object content, string header, IWorkItem owner)
        {
            DXTabItem item = new DXTabItem() { Content = content, Header = header };
            if (owner != null)
            {
                SetTabHeader(item, header, owner.IsDirty);

                WorkitemManager.SetOwner(item, owner);
                owner.IsDirtyChanged += (o, e) =>
                {
                    SetTabHeader(item, header, owner.IsDirty);
                };
            }
            regionManager.AddToRegion(Constants.RegionNames.TabRegion, item);
            return item;
        }

        private static void SetTabHeader(DXTabItem item, string header, bool isDirty)
        {
            if (isDirty)
                item.Header = header + "*";
            else
                item.Header = header;
        }

        public static void RemoveTab(this IRegionManager regionManager, IWorkItem workItem)
        {
            regionManager.RemoveWorkitemFromRegion(regionManager.Regions[Constants.RegionNames.TabRegion], workItem);
        }
    }
}
