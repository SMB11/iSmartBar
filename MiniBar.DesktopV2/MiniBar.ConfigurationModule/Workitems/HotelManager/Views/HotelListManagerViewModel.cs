using MiniBar.EntityViewModels.Configuration;
using System.Collections.ObjectModel;

namespace MiniBar.ConfigurationModule.Workitems.HotelManager.Views
{
    partial class HotelManagerViewModel
    {

        private ObservableCollection<HotelViewModel> items;
        public override ObservableCollection<HotelViewModel> ListItems
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value, nameof(ListItems));
            }
        }
    }
}
