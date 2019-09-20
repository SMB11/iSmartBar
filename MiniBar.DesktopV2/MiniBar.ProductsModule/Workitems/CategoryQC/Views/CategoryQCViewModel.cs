using MiniBar.Common.Workitems.EntityQC.Views;
using MiniBar.EntityViewModels.Products;
using System.Collections.ObjectModel;

namespace MiniBar.ProductsModule.Workitems.CategoryQC.Views
{
    public class CategoryQCViewModel : QCViewModel
    {


        private ObservableCollection<CategoryViewModel> categories = new ObservableCollection<CategoryViewModel>();
        public ObservableCollection<CategoryViewModel> RootCategories
        {
            get { return categories; }
            set
            {
                SetProperty(ref categories, value, nameof(RootCategories));
            }
        }
    }
}