using MiniBar.EntityViewModels.Configuration;
using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
