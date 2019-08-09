using Infrastructure.Framework;
using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Configuration
{

    public class HotelViewModel : EntityViewModelBase<HotelViewModel>, IIdEntityViewModel
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

        private string city;
        [GridColumn]
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value, nameof(City)); }
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