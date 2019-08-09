using Infrastructure.Interface;
using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Configuration
{
    public class HotelUploadViewModel : EntityViewModelBase<HotelUploadViewModel>, IIdEntityViewModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value, nameof(ID)); }
        }


        private int cityid;

        public int CityID
        {
            get { return cityid; }
            set { SetProperty(ref cityid, value, nameof(CityID)); }
        }


        private CityViewModel city;

        public CityViewModel City
        {
            get { return city; }
            set { SetProperty(ref city, value, nameof(City)); }
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
        public override string ToString()
        {
            return Name;
        }
    }
}
