using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Infrastructure.Interface;
using System.Linq;
using MiniBar.ProductsModule.Workitems.ProductManager;
using Infrastructure.MVVM;
using MiniBar.ProductsModule.Workitems.CategoryManager;
using MiniBar.ProductsModule.Workitems.BrandManager;

namespace MiniBar.ProductsModule.Resources
{
    public class MainPageMenuGroupViewModel : BaseViewModel
    {
        public MainPageMenuGroupViewModel(ICurrentContextService currentContextService)
        {
            currentContextService.WorkitemCollectionChanged += CurrentContextService_WorkitemCollectionChanged;
        }

        private void CurrentContextService_WorkitemCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        ProductManagerCount += e.NewItems.OfType<ProductManagerWorkitem>().Count();
                        BrandManagerCount += e.NewItems.OfType<BrandManagerWorkitem>().Count();
                        CategoryManagerCount += e.NewItems.OfType<CategoryManagerWorkitem>().Count();
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems!= null)
                    {
                        ProductManagerCount -= e.OldItems.OfType<ProductManagerWorkitem>().Count();
                        BrandManagerCount -= e.OldItems.OfType<BrandManagerWorkitem>().Count();
                        CategoryManagerCount -= e.OldItems.OfType<CategoryManagerWorkitem>().Count();
                    }
                    break;
            }
        }

        private int productManagerCount;

        public int ProductManagerCount
        {
            get { return productManagerCount; }
            set {
                SetProperty(ref productManagerCount,  value, nameof(ProductManagerCount));
                RaisePropertyChanged(nameof(ProductManagerInformativeText));
            }
        }

        public string ProductManagerInformativeText
        {
            get
            {
                if (ProductManagerCount > 0)
                    return ProductManagerCount + " open";
                return null;
            }
        }


        private int brandManagerCount;

        public int BrandManagerCount
        {
            get { return brandManagerCount; }
            set
            {
                SetProperty(ref brandManagerCount, value, nameof(BrandManagerCount));
                RaisePropertyChanged(nameof(BrandManagerInformativeText));
            }
        }

        public string BrandManagerInformativeText
        {
            get
            {
                if (BrandManagerCount > 0)
                    return BrandManagerCount + " open";
                return null;
            }
        }

        private int categoryManagerCount;

        public int CategoryManagerCount
        {
            get { return categoryManagerCount; }
            set
            {
                SetProperty(ref categoryManagerCount, value, nameof(CategoryManagerCount));
                RaisePropertyChanged(nameof(CategoryManagerInformativeText));
            }
        }

        public string CategoryManagerInformativeText
        {
            get
            {
                if (CategoryManagerCount > 0)
                    return CategoryManagerCount + " open";
                return null;
            }
        }

    }
}