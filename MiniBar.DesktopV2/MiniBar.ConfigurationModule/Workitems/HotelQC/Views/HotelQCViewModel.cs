using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using MiniBar.Common.Workitems.EntityQC.Views;
using System.Collections.ObjectModel;
using MiniBar.EntityViewModels.Configuration;

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