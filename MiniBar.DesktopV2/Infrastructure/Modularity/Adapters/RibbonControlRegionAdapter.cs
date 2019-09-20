using DevExpress.Xpf.Ribbon;
using Prism.Regions;
using System.Collections.Specialized;
using System.Linq;

namespace Infrastructure.Modularity
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
                bool isFirstAdd = true;
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (RibbonPageCategoryBase cat in e.NewItems.OfType<RibbonPageCategoryBase>())
                        {
                            regionTarget.Items.Add(cat);
                            var page = cat.GetFirstSelectablePage();
                            if (page != null && isFirstAdd)
                                regionTarget.SelectedPage = page;
                            isFirstAdd = false;
                        }

                        foreach (var toolbar in e.NewItems.OfType<BarLinkHolder>())
                        {
                            foreach (var item in toolbar.Items)
                            {
                                regionTarget.ToolbarItems.Add(item);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (RibbonPageCategoryBase cat in e.OldItems.OfType<RibbonPageCategoryBase>())
                        {
                            regionTarget.Items.Remove(cat);
                        }

                        foreach (var toolbar in e.OldItems.OfType<BarLinkHolder>())
                        {
                            foreach (var item in toolbar.Items)
                            {
                                regionTarget.ToolbarItems.Remove(item);
                            }
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}
