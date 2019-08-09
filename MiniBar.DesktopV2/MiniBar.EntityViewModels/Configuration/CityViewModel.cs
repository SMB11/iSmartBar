using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Configuration
{
    public class CityViewModel : EntityViewModelBase<CityViewModel>, IIdEntityViewModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value, nameof(ID)); }
        }

        private int countryid;

        public int CountryID
        {
            get { return countryid; }
            set { SetProperty(ref countryid, value, nameof(CountryID)); }
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

        private string country;
        [GridColumn]
        public string Country
        {
            get { return country; }
            set { SetProperty(ref country, value, nameof(Country)); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
