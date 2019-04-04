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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void ActivateMainWindow(IntPtr hWnd)
        {

            SetForegroundWindow(hWnd);

            // If program is minimized, restore it.
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }
        }

        protected override Window CreateShell()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            DXSplashScreen.Show<SplashScreen>();
            ShellWindow window = Container.Resolve<ShellWindow>();
            window.Loaded += (o, e) =>
            {

                DXSplashScreen.Close();
                window.Activate();
                window.Focus();
                window.Topmost = true;
                window.Topmost = false;
                IntPtr handle = new WindowInteropHelper(window).Handle;
                ActivateMainWindow(handle);
            };
            return window;
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
            containerRegistry.RegisterSingleton(typeof(ICurrentContextService), typeof(CurrentContextService));

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
            
            Container.Resolve<IModuleManager>().LoadModuleCompleted += (o, e) =>
            {
                DXSplashScreen.SetState(String.Format("Loading {0}", e.ModuleInfo.ModuleName));
            };
            moduleCatalog.AddModule<SecurityModule>();
            moduleCatalog.AddModule<ProductsModule>();
            
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                if (viewName.EndsWith("view", StringComparison.CurrentCultureIgnoreCase))
                {
                    viewName = viewName.Remove(viewName.Length - 4, 4);
                }
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);

            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.CurrencySymbol = "€";
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}
