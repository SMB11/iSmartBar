using DevExpress.Xpf.Bars;
using Prism.Regions;
using System.Collections.Specialized;

namespace Infrastructure.Modularity
{

    public class BarContainerControlRegionAdapter : RegionAdapterBase<BarContainerControl>
    {
        public BarContainerControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, BarContainerControl regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (ToolBarControl element in e.NewItems)
                        {
                            regionTarget.Bars.Add(element);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (ToolBarControl elementLoopVariable in e.OldItems)
                        {
                            var element = elementLoopVariable;
                            if (regionTarget.Bars.Contains(element))
                            {
                                regionTarget.Bars.Remove(element);
                            }
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
