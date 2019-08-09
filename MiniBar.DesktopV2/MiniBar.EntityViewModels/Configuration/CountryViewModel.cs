using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Configuration
{
    public class CountryViewModel : EntityViewModelBase<CountryViewModel>, IIdEntityViewModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value, nameof(ID)); }
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
