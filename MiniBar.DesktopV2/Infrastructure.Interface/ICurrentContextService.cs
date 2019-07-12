
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Infrastructure.Interface
{
    public interface ICurrentContextService
    {
        void LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        void FocusWorkitem(IWorkItem workItem);
        bool CloseWorkitem(IWorkItem workItem);
        event NotifyCollectionChangedEventHandler WorkitemCollectionChanged;
        string ShellTitle { get; set; }
    }
}
