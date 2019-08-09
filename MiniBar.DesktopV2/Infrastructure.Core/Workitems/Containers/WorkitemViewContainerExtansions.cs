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

        public static TView Register<TView>(this IViewContainer viewContainer, TView view, ScreenRegion region)
        {
            return (TView)viewContainer.Register(view, region);
        }
    }
}
