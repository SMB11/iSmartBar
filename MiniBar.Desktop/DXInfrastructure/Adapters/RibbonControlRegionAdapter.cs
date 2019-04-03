using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXInfrastructure.Adapters
{
    public class RibbonControlRegionAdapter : RegionAdapterBase<RibbonControl>
    {
        public RibbonControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RibbonControl regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (RibbonPageCategory cat in e.NewItems)
                        {
                            regionTarget.Items.Add(cat);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (RibbonPageCategory cat in e.OldItems)
                        {
                            regionTarget.Items.Remove(cat);
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
