using MiniBar.EntityViewModels.Configuration;
using System.Collections.ObjectModel;

namespace MiniBar.ConfigurationModule.Workitems.CityManager.Views
{
    partial class CityManagerViewModel
    {

        private ObservableCollection<CityViewModel> items;
        public override ObservableCollection<CityViewModel> ListItems
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value, nameof(ListItems));
            }
        }
    }
}
