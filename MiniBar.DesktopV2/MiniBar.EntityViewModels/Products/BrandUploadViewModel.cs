using Infrastructure.Interface;

using MiniBar.EntityViewModels.Base;
using MiniBar.EntityViewModels.Global;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Products
{
    public class BrandUplaodViewModel : EntityViewModelBase<BrandUplaodViewModel>, IIdEntityViewModel
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                SetProperty(ref _ID, value, nameof(ID));
            }
        }

        private string _Name;
        [Required]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        private ImageViewModel _image = new ImageViewModel();
        public ImageViewModel Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value, nameof(Image));
            }
        }
    }
}
