using Core.UI;
using DevExpress.Xpf.LayoutControl;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Windows;

namespace Infrastructure.Modularity
{
    public class FlowLayoutControlRegionAdapter : RegionAdapterBase<FlowLayoutControl>
    {
        public FlowLayoutControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, FlowLayoutControl regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (UIElement element in e.NewItems)
                        {
                            if (element is TileCollection)
                            {
                                TileCollection tiles = element as TileCollection;
                                tiles.Tag = Guid.NewGuid();
                                for (int i = 0; i < tiles.Items.Count; i++)
                                {
                                    if (tiles.Items[i] is InformativeTile)
                                    {
                                        InformativeTile tile = tiles.Items[i] as InformativeTile;
                                        tile.Tag = tiles.Tag;
                                        tiles.Items.Remove(tile);
                                        tile.DataContext = tiles.DataContext;
                                        i--;
                                        regionTarget.Children.Add(tile as UIElement);
                                    }
                                }
                            }
                            else
                                regionTarget.Children.Add(element);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (UIElement element in e.OldItems)
                        {
                            regionTarget.Children.Remove(element);
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
