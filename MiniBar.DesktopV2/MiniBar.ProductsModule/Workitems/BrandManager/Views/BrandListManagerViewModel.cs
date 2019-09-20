using MiniBar.EntityViewModels.Products;
using System.Collections.ObjectModel;

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
