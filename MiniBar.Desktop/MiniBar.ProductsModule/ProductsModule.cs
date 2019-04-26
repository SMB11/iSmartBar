using DXInfrastructure;
using MiniBar.ProductsModule;
using MiniBar.ProductsModule.Constants;
using MiniBar.ProductsModule.Workitems.ProductManager;
using MiniBar.ProductsModule.Workitems.ProductManager.Views;
using Prism.Ioc;
using Prism.Regions;
using Unity.Attributes;
using MiniBar.ProductsModule.Services;
using Security;
using Infrastructure.MVVM.Commands;
using MiniBar.ProductsModule.Workitems.BrandManager;

namespace MiniBar.Modules
{
    [SecureModule]
    public class ProductsModule : Module
    {

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public override string Name => "Products";

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.NavBarControlRegion, typeof(NavBarControls));
            BarCommandManager.RegisterCommand(CommandNames.OpenProductManager, new SecureCommand(OpenProductManager));
            BarCommandManager.RegisterCommand(CommandNames.OpenBrandManager, new SecureCommand(OpenBrandManager));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<ProductService>();
            containerRegistry.RegisterSingleton<BrandService>();
            containerRegistry.RegisterSingleton<CategoryService>();

            containerRegistry.Register<ProductManagerWorkitem>();
            containerRegistry.Register<ProductListView>();
            containerRegistry.Register<ProductDetailsView>();
            containerRegistry.Register<BrandManagerWorkitem>();
            containerRegistry.Register<BrandDetailsView>();
            containerRegistry.Register<BrandListView>();

        }

        private void OpenProductManager()
        {
            CurrentContextService.LaunchWorkItem<ProductManagerWorkitem>();
        }


        private void OpenBrandManager()
        {
            CurrentContextService.LaunchWorkItem<BrandManagerWorkitem>();
        }
    }
}
