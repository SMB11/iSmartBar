using System;

namespace Infrastructure.Interface
{
    public interface IObjectListManager
    {
        bool IsListLoading { get; set; }
        object CurrentItem { get; }
        IObservable<object> WhenCurrentItemChanges { get; }


        void RemoveByID(int id);
        void RefreshItems(int? selectID = null);
        void Disable();
        void Enable();
    }
}
