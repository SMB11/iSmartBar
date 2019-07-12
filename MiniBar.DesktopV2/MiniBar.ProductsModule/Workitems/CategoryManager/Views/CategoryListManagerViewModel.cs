using MiniBar.Common.Services;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Workitems.CatgeoryManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
