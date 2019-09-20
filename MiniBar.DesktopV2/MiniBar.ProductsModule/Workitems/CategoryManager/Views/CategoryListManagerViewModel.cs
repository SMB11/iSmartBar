using MiniBar.EntityViewModels.Products;
using System.Collections.ObjectModel;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    partial class CategoryManagerViewModel
    {

        private ObservableCollection<CategoryViewModel> rootCategories = new ObservableCollection<CategoryViewModel>();
        public ObservableCollection<CategoryViewModel> RootCategories
        {
            get { return rootCategories; }
            set
            {
                SetProperty(ref rootCategories, value, nameof(RootCategories));
            }
        }


        private ObservableCollection<CategoryViewModel> childCategories = new ObservableCollection<CategoryViewModel>();
        public override ObservableCollection<CategoryViewModel> ListItems
        {
            get { return childCategories; }
            set
            {
                SetProperty(ref childCategories, value, nameof(ListItems));
            }
        }

    }
}
