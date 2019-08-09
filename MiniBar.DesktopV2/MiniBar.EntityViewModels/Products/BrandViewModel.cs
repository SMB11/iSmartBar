using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Products
{
    public class BrandViewModel : EntityViewModelBase<BrandViewModel>, IIdEntityViewModel
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
        [GridColumn]
        [Required]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
