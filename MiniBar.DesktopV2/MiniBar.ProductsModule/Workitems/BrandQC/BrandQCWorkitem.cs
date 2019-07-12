using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using MiniBar.Common.Workitems.EntityQC;
using MiniBar.EntityViewModels.Products;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.ProductsModule.Workitems.BrandQC
{
    class BrandQCWorkitem : EntityQCWorkitem<BrandUplaodViewModel>
    {
        public BrandQCWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Brand Quality Control";

        protected override FrameworkElement CreateView()
        {
            return new Views.BrandQCView(this);
        }
    }
}
