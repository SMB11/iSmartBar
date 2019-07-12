using System;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Interface
{
    public interface IWorkItem : ISupportsFocus
    {
        event EventHandler<EventArgs> IsDirtyChanged;
        Task OnResultRecieved(IWorkItem child, object result);
        string WorkItemName { get; }
        bool IsOpen { get; set; }
        string WorkItemID { get; }
        bool IsDirty { get; }
        void Run();
        bool Close();
        void Cleanup();
        Window Window { get; set; }
        IWorkItem Parent { get; set; }
        object RequestResource(string name);
    }
}
