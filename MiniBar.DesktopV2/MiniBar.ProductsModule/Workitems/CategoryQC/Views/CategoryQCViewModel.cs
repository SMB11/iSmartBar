using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using MiniBar.Common.Workitems.EntityQC.Views;
using System.Collections.ObjectModel;
using MiniBar.EntityViewModels.Products;

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