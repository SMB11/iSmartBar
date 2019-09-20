namespace Infrastructure.Interface
{
    public interface IShell
    {

        void ShowLoading(string action);
        void EndLoading();
    }
}
