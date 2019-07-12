using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using MiniBar.Common.Workitems.EntityQC.Views;
using System.Collections.ObjectModel;
using MiniBar.EntityViewModels.Products;

namespace MiniBar.ProductsModule.Workitems.ProductQC.Views
{
    public class ProductQCViewModel : QCViewModel
    {
        private ObservableCollection<CategoryViewModel> categories = new ObservableCollection<CategoryViewModel>();
        public ObservableCollection<CategoryViewModel> ChildCategories
        {
            get { return categories; }
            set
            {
                SetProperty(ref categories, value, nameof(ChildCategories));
            }
        }


        private ObservableCollection<BrandViewModel> brands = new ObservableCollection<BrandViewModel>();
        public ObservableCollection<BrandViewModel> Brands
        {
            get { return brands; }
            set
            {
                SetProperty(ref brands, value, nameof(Brands));
            }
        }
    }
}