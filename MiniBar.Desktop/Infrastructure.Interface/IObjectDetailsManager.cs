namespace Infrastructure.Interface
{
    public interface IObjectDetailsManager
    {
        bool IsObjectLoading { get; set; }
        object EditingItem { get; }

        void ChangeCurrentItem(object currentItem);
        void BeginAdd();
        void BeginEdit();
        void Cancel();
    }
}
