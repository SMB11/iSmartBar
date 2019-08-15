using Infrastructure.Interface;
using Infrastructure.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{
    public static class WorkitemViewContainerExtansions
    {

        /// <summary>
        /// Register a veiw and return it
        /// </summary>
        /// <typeparam name="TView">Type of view</typeparam>
        /// <param name="viewContainer">The view container</param>
        /// <param name="view">The view</param>
        /// <param name="region">The region to add into</param>
        /// <returns></returns>
        public static TView Register<TView>(this IViewContainer viewContainer, TView view, ScreenRegion region)
        {
            return (TView)viewContainer.Register(view, region);
        }
    }
}
