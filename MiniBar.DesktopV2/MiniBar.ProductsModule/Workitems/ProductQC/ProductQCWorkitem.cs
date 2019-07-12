using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using Infrastructure.Util;
using MiniBar.Common.Workitems.EntityQC;
using MiniBar.EntityViewModels.Global;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Workitems.ProductQC.Views;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.ProductsModule.Workitems.ProductQC
{
    class ProductQCWorkitem : EntityQCWorkitem<ProductUploadViewModel>
    {
        public ProductQCWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Product Quality Control";

        protected override FrameworkElement CreateView()
        {
            return new ProductQC.Views.ProductQCView(this);
        }
        
        private ProductQCViewModel ProductQCViewModel
        {
            get
            {
                return QCViewModel as ProductQCViewModel;
            }
        }

        public override void Run()
        {
            base.Run();
            object root = Parent.RequestResource("ChildCategories");
            if (root != null && root is IEnumerable<CategoryViewModel>)
                ProductQCViewModel.ChildCategories = new ObservableCollection<CategoryViewModel>((IEnumerable<CategoryViewModel>)root);

            object brands = Parent.RequestResource("Brands");
            if (brands != null && brands is IEnumerable<BrandViewModel>)
                ProductQCViewModel.Brands = new ObservableCollection<BrandViewModel>((IEnumerable<BrandViewModel>)brands);
        }

        
        public override void Initialize(object data)
        {
            base.Initialize(data);
            foreach(var item in List.OfType<ProductUploadViewModel>())
            {

                var dict = new BindableDictionary<BindableTuple<string, string>>();
                foreach (string key in item.Names.Keys)
                {
                    if (item.Description.ContainsKey(key))
                        dict.Add(key, new BindableTuple<string, string>(item.Names[key], item.Description[key]));
                    else
                        dict.Add(key, new BindableTuple<string, string>(item.Names[key], ""));
                }
                item.LanguageData = dict;
            }
        }
    }
}
