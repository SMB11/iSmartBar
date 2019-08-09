using MiniBar.ProductsModule;
using MiniBar.ProductsModule.Constants;
using MiniBar.ProductsModule.Workitems.ProductManager;
using Prism.Ioc;
using Prism.Regions;
using MiniBar.ProductsModule.Services;
using Security;
using MiniBar.ProductsModule.Workitems.BrandManager;
using Infrastructure;
using MiniBar.ProductsModule.Workitems.CategoryManager;
using MiniBar.ProductsModule.Resources;
using Infrastructure.Workitems;
using System.Threading.Tasks;
using Infrastructure.Framework;

namespace MiniBar.Modules
{
    [SecureModule]
    public class ProductsModule : Module
    {

        public IRegionManager RegionManager { get; set; }

        public ProductsModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public override string Name => "Products";

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CommandManager.RegisterCommand(CommandNames.OpenProductManager, new SecureAsyncCommand<bool>(OpenProductManager));
            CommandManager.RegisterCommand(CommandNames.OpenBrandManager, new SecureAsyncCommand<bool>(OpenBrandManager));
            CommandManager.RegisterCommand(CommandNames.OpenCategoryManager, new SecureAsyncCommand<bool>(OpenCategoryManager));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<ProductService>();
            containerRegistry.RegisterSingleton<BrandService>();
            containerRegistry.RegisterSingleton<CategoryService>();

            containerRegistry.Register<ProductManagerWorkitem>();
            containerRegistry.Register<BrandManagerWorkitem>();
            containerRegistry.Register<CategoryManagerWorkitem>();

            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.NavBarControlRegion, typeof(NavBarControls));
            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.MainPageMenuRegion, typeof(MainPageMenuGroup));
            
        }

        private async Task OpenProductManager(bool isModal)
        {
            if(isModal)
                await CurrentContextService.LaunchModalWorkItem<ProductManagerWorkitem>();
            else
                await CurrentContextService.LaunchWorkItem<ProductManagerWorkitem>();
        }

        private async Task OpenBrandManager(bool isModal)
        {

            if (isModal)
                await CurrentContextService.LaunchModalWorkItem<BrandManagerWorkitem>();
            else
                await CurrentContextService.LaunchWorkItem<BrandManagerWorkitem>();
        }

        private async Task OpenCategoryManager(bool isModal)
        {
            if (isModal)
                await CurrentContextService.LaunchModalWorkItem<CategoryManagerWorkitem>();
            else
                await CurrentContextService.LaunchWorkItem<CategoryManagerWorkitem>();
        }
    }
}
