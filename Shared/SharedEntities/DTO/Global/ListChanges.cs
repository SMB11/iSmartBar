using System;
using System.Collections.Generic;

namespace SharedEntities.DTO.Global
{
    public class ListChanges<T>
    {
        public ListChanges(List<T> newItems, List<T> old, List<T> changed)
        {
            NewItems = newItems;
            OldItems = old;
            ChangedItems = changed;
        }

        public List<T> NewItems { get; set; }
        public List<T> OldItems { get; set; }
        public List<T> ChangedItems { get; set; }
    }
}
