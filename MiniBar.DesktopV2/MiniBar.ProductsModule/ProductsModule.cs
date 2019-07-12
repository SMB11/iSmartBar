using MiniBar.ProductsModule;
using MiniBar.ProductsModule.Constants;
using MiniBar.ProductsModule.Workitems.ProductManager;
using MiniBar.ProductsModule.Workitems.ProductManager.Views;
using Prism.Ioc;
using Prism.Regions;
using Unity.Attributes;
using MiniBar.ProductsModule.Services;
using Security;
using MiniBar.ProductsModule.Workitems.BrandManager;
using Infrastructure;
using Infrastructure.MVVM;
using MiniBar.ProductsModule.Workitems.CategoryManager;
using MiniBar.ProductsModule.Resources;
using AutoMapper;

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
            CommandManager.RegisterCommand(CommandNames.OpenProductManager, new SecureCommand(OpenProductManager));
            CommandManager.RegisterCommand(CommandNames.OpenBrandManager, new SecureCommand(OpenBrandManager));
            CommandManager.RegisterCommand(CommandNames.OpenCategoryManager, new SecureCommand(OpenCategoryManager));
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

        private void OpenProductManager()
        {
            CurrentContextService.LaunchWorkItem<ProductManagerWorkitem>();
        }


        private void OpenBrandManager()
        {
            CurrentContextService.LaunchWorkItem<BrandManagerWorkitem>();
        }


        private void OpenCategoryManager()
        {
            CurrentContextService.LaunchWorkItem<CategoryManagerWorkitem>();
        }
    }
}
