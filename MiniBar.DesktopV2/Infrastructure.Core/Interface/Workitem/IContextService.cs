using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IContextService
    {
        Task<IObservable<WorkitemEventArgs>> LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task FocusWorkitem(IWorkItem workItem);
        Task<bool> CloseWorkitem(IWorkItem workItem);
        event NotifyCollectionChangedEventHandler WorkitemCollectionChanged;
    }
}
