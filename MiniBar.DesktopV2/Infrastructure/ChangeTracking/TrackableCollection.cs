using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ChangeTracking
{
    public class TrackableCollection<T> : ObservableCollection<T>, IChangeTracking, INotifyPropertyChanged
        where T: INotifyPropertyChanged, IChangeTracking
    {
        public TrackableCollection()
        {
        }

        public TrackableCollection(List<T> list) : base(list)
        {
            AcceptChanges();
        }

        public TrackableCollection(IEnumerable<T> collection) : base(collection)
        {
            AcceptChanges();
        }

        private bool isChanged;
        public bool IsChanged
        {
            get
            {
                return isChanged;
            }
            private set
            {
                isChanged = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            }
        }

        public void AcceptChanges()
        {
            foreach(var item in this)
                item.AcceptChanges();

            IsChanged = false;
            NewItems = new List<T>();
            OldItems = new List<T>();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    NewItems.AddRange(e.NewItems.OfType<T>());
                    IsChanged = true;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    IsChanged = true;
                    foreach (T item in e.OldItems.OfType<T>()) {
                        if (NewItems.Contains(item))
                            NewItems.Remove(item);
                        else
                            OldItems.Add(item);
                    }
                    break;
                default:
                    break;
            }
        }

        public List<T> NewItems { get; private set; } = new List<T>();
        
        public List<T> OldItems { get; private set; } = new List<T>();

    }
}
