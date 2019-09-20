using MiniBar.EntityViewModels.Configuration;
using System.Collections.ObjectModel;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Views
{
    partial class CountryManagerViewModel
    {

        private ObservableCollection<CountryViewModel> items;
        public override ObservableCollection<CountryViewModel> ListItems
        {
            get { return items; }
            set
            {
                SetProperty(ref items, value, nameof(ListItems));
            }
        }
    }
}
