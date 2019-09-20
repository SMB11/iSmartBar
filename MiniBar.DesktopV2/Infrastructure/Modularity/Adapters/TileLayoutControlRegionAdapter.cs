using Core.UI;
using DevExpress.Xpf.LayoutControl;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Infrastructure.Modularity
{
    public class TileLayoutControlRegionAdapter : RegionAdapterBase<TileLayoutControl>
    {
        public TileLayoutControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }


        protected override void Adapt(IRegion region, TileLayoutControl control)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (TileCollection tiles in e.NewItems.OfType<TileCollection>())
                        {
                            tiles.Tag = Guid.NewGuid();
                            for (int i = 0; i < tiles.Items.Count; i++)
                            {
                                if (tiles.Items[i] is InformativeTile)
                                {
                                    InformativeTile tile = tiles.Items[i] as InformativeTile;
                                    tile.Tag = tiles.Tag;
                                    tiles.Items.Remove(tile);
                                    i--;
                                    control.Children.Add(tile as UIElement);
                                }
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (TileCollection tiles in e.OldItems.OfType<TileCollection>())
                        {
                            var toRemove = control.Children
                                                .OfType<Tile>()
                                                .Where(t =>
                                                    t.Tag != null &&
                                                    t.Tag.Equals(tiles.Tag)
                                                ).ToList();
                            for (int i = 0; i < toRemove.Count; i++)
                            {
                                var tile = toRemove[i];
                                if (tile is UIElement)
                                {
                                    control.Children.Remove(tile as UIElement);
                                }
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
