
namespace Infrastructure.Interface
{
    public interface ICurrentContextService
    {
        void LaunchWorkItem<T>() where T : IWorkItem;
        bool CloseCurrentWorkItem();
    }
}
