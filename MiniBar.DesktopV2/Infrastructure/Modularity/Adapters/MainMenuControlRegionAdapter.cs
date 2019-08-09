using DevExpress.Xpf.Bars;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Infrastructure.Modularity
{
    public class MainMenuControlRegionAdapter : RegionAdapterBase<MainMenuControl>
    {
        public MainMenuControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, MainMenuControl regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (BarSubItem element in e.NewItems)
                        {
                            BarSubItem found = regionTarget.Items.OfType<BarSubItem>().Where(item => item.Content == element.Content && item.Tag.Equals(element.Tag)).FirstOrDefault();
                            if(found == null) regionTarget.Items.Insert(0, element);
                            else
                            {
                                found.Items.AddRange(element.Items);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (BarItem elementLoopVariable in e.OldItems)
                        {
                            var element = elementLoopVariable;
                            if (regionTarget.Items.Contains(element))
                            {
                                regionTarget.Items.Remove(element);
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
