using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Prism
{
    public static class RegionManagerExtensions
    {
        public static void RemoveWorkitemViews(this IRegionManager regionManager, string id)
        {
            foreach(IRegion region in regionManager.Regions){
                foreach(var view in region.Views)
                {
                    if(view is FrameworkElement)
                        if((view as FrameworkElement).Tag?.ToString() == id)
                            region.Remove(view);
                    if (view is FrameworkContentElement)
                        if ((view as FrameworkContentElement).Tag?.ToString() == id)
                            region.Remove(view);
                }
            }
        }

        public static void ClearNavigation(this IRegionManager regionManager, string region)
        {
            regionManager.Regions[region].RemoveAll();
            regionManager.Regions[region].NavigationService.Journal.Clear();
        }
    }
}
