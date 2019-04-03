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
            BarCommandManager.RegisterCommand(CommandNames.OpenProductManager, ToSecureCommand(OpenProductManager));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<ProductService>();
            containerRegistry.RegisterSingleton<CategoryService>();

            containerRegistry.Register<ProductManagerWorkitem>();
            containerRegistry.Register<ProductListView>();
            containerRegistry.Register<ProductDetailsView>();

        }

        private void OpenProductManager()
        {
            CurrentContextService.LaunchWorkItem<ProductManagerWorkitem>();
        }
    }
}
