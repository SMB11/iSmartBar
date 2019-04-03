using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Prism;
using DXInfrastructure.Adapters;
using MiniBar.Modules;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.WindowsUI;
using AutoMapper;
using Prism.Mvvm;
using System.Reflection;
using System.Globalization;
using Infrastructure.Interface;
using Security.Api;
using Infrastructure.Helpers;
using Infrastructure.Workitems;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            return Container.Resolve<ShellWindow>();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if(e.Exception is ApiConnectionException)
            {
                e.Handled = true;

                UIHelper.ShowErrorMessageBox(e.Exception.Message);
                Shutdown();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                if(viewName.EndsWith("view", StringComparison.CurrentCultureIgnoreCase))
                {
                    viewName = viewName.Remove(viewName.Length - 4, 4);
                }
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
            containerRegistry.RegisterSingleton(typeof(ICurrentContextService), typeof(CurrentContextService));
            DXSplashScreenService dXSplashScreenService = new DXSplashScreenService();
            //dXSplashScreenService.ShowSplashScreenOnLoading = true;
            containerRegistry.RegisterInstance<ISplashScreenService>(dXSplashScreenService);

            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(App).Assembly));
            Mapper.AssertConfigurationIsValid();

        }


        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
        {
            base.ConfigureRegionAdapterMappings(mappings);
            var factory = Container.Resolve<IRegionBehaviorFactory>();
            
            mappings.RegisterMapping(typeof(NavBarControl),
                DevExpress.Xpf.Prism.AdapterFactory.Make<RegionAdapterBase<NavBarControl>>(factory));

            mappings.RegisterMapping(typeof(NavigationFrame),
                DevExpress.Xpf.Prism.AdapterFactory.Make<RegionAdapterBase<NavigationFrame>>(factory));
            mappings.RegisterMapping(typeof(MainMenuControl), 
                new MainMenuControlRegionAdapter(factory));
            mappings.RegisterMapping(typeof(BarContainerControl),
                new BarContainerControlRegionAdapter(factory));

            mappings.RegisterMapping(typeof(RibbonControl),
                new RibbonControlRegionAdapter(factory));

            DXRegionManager.PrismVersion = PrismVersion.Prism7;

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SecurityModule>();
            moduleCatalog.AddModule<ProductsModule>();
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            
            culture.NumberFormat.CurrencySymbol = "€";
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            
        }
    }
}
