using MiniBar.EntityViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.BrandManager.Views
{
    partial class BrandManagerViewModel
    {

        private ObservableCollection<BrandViewModel> brands;
        public override ObservableCollection<BrandViewModel> ListItems
        {
            get { return brands; }
            set
            {
                SetProperty(ref brands, value, nameof(ListItems));
            }
        }
    }
}
