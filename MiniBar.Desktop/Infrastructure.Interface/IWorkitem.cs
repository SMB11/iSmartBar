namespace Infrastructure.Interface
{
    public interface IWorkItem
    {
        void Run();
        bool CanClose();
        void Cleanup();
    }
}
