using MiniBar.EntityViewModels.Base;

namespace MiniBar.EntityViewModels.Global
{
    public class ImageViewModel : EntityViewModelBase<ImageViewModel>
    {
        private byte[] bytes;
        public byte[] Bytes
        {
            get
            {
                return bytes;
            }
            set
            {
                SetProperty(ref bytes, value, nameof(Bytes));
            }
        }
    }
}
