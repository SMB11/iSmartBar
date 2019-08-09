using MiniBar.EntityViewModels.Configuration;
using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
