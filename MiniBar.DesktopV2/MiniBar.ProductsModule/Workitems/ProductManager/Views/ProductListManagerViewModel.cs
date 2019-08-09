
using System.Collections.ObjectModel;
using MiniBar.EntityViewModels.Products;
using MiniBar.Common.MVVM;
using MiniBar.Common.Workitems.ObjectManager;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    /// <summary>
    /// List Part
    /// </summary>
    partial class ProductManagerViewModel : ObjectManagerViewModel<ProductViewModel, ProductUploadViewModel>
    {

        private ObservableCollection<ProductViewModel> products;

        public override ObservableCollection<ProductViewModel> ListItems
        {
            get { return products; }
            set { SetProperty(ref products, value, nameof(ListItems)); }
        }

    }
}