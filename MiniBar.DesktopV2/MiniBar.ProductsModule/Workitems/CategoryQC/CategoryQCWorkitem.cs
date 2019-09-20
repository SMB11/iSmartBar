
using MiniBar.Common.Workitems.EntityQC;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Workitems.CategoryQC.Views;
using Prism.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MiniBar.ProductsModule.Workitems.CategoryQC
{
    class CategoryQCWorkitem : EntityQCWorkitem<CategoryUploadViewModel>
    {
        public CategoryQCWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Category Quality Control";

        private CategoryQCViewModel CategoryQCViewModel
        {
            get
            {
                return QCViewModel as CategoryQCViewModel;
            }
        }

        protected override FrameworkElement CreateView()
        {
            return new CategoryQC.Views.CategoryQCView(this);
        }

        protected override void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
            object root = Parent.RequestResource("RootCategories");
            if (root != null && root is IEnumerable<CategoryViewModel>)
                CategoryQCViewModel.RootCategories = new ObservableCollection<CategoryViewModel>((IEnumerable<CategoryViewModel>)root);
        }
    }
}
