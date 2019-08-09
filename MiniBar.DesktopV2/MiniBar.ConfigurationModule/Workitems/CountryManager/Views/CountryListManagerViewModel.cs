using MiniBar.EntityViewModels.Configuration;
using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
