using MiniBar.Common.Workitems.EntityQC.Views;
using MiniBar.EntityViewModels.Configuration;
using System.Collections.ObjectModel;

namespace MiniBar.ConfigurationModule.Workitems.HotelQC.Views
{
    public class HotelQCViewModel : QCViewModel
    {

        private ObservableCollection<CityViewModel> cities;
        public ObservableCollection<CityViewModel> Cities
        {
            get { return cities; }
            set
            {
                SetProperty(ref cities, value, nameof(Cities));
            }
        }
    }
}