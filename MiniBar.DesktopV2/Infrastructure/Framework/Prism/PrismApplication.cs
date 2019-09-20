using AutoMapper;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Prism;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.WindowsUI;
using Infrastructure.ErrorHandling;
using Infrastructure.Interface;
using Infrastructure.Logging;
using Infrastructure.Modularity;
using Infrastructure.Security;
using Infrastructure.Utility;
using Infrastructure.Workitems;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.IO;

namespace Infrastructure.Framework
{
    /// <summary>
    /// Prism Applciation base class based on DryIoc Container implementation
    /// </summary>
    public abstract class PrismApplication : Prism.Unity.PrismApplication
    {
        protected IExceptionHandler ExceptionHandler => Container.Resolve<IExceptionHandler>();
        protected ICompositeLogger Logger => Container.Resolve<ICompositeLogger>();
        protected IUIManager UIManager => Container.Resolve<IUIManager>();

        public bool IsDebug { get; private set; }

        public PrismApplication()
        {
            #if DEBUG
                IsDebug = true;
            #endif
        }

        /// <summary>
        /// Contains actions that should occur last.
        /// </summary> 
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.Log("Apllication has been initialized", LogLevel.Informative);
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppSecurityContext.AppPrincipalChanged += AppSecurityContext_AppPrincipalChanged;
        }

        private void AppSecurityContext_AppPrincipalChanged(object sender, EventArgs e)
        {
            if (AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                OnAuthenticated();
        }

        protected virtual void OnAuthenticated()
        {

        }

        /// <summary>
        /// Handle user-unhandled exceptions
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Logger.Log(e.Exception.ToString(), LogLevel.Exception);
            ExceptionHandler.HandleError(e.Exception);
            UIManager.Error(e.Exception.Message);
            Shutdown();
        }

        /// <summary>
        /// Register main types with the container
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            MapperConfiguration configuration = new MapperConfiguration(ConfigureMappings);
            containerRegistry
                .AddLogging(opt =>
                {
                    opt.AddDebug();
                    opt.AddFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyBar", "Log", $"log-{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt"));
                })
                .AddDefaultExceptionHandling()
                .RegisterSingleton(typeof(ITaskManager), typeof(BaseTaskManager))
                .RegisterSingleton(typeof(IUIManager), typeof(UIManager))
                .RegisterSingleton(typeof(IContextService), typeof(ContextService))
                .RegisterSingleton(typeof(IConnectionMonitoringService), typeof(ConnectionMonitoringService))
                .RegisterInstance<IMapper>(configuration.CreateMapper());

        }

        /// <summary>
        /// Create DynamicDirectoryModuleCatalog that provides dynamic loading capabilites
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            DynamicDirectoryModuleCatalog catalog = new DynamicDirectoryModuleCatalog(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Modules"), Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin"));
            return catalog;
        }

        /// <summary>
        /// Configure how the ViewModelLocator resolves ViewModels
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProviderExtensions.SetCustomViewTypeToViewModelTypeResolver();
        }

        /// <summary>
        /// Configure Region Adapters
        /// </summary>
        /// <param name="mappings"></param>
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
        {
            base.ConfigureRegionAdapterMappings(mappings);
            Logger.Log("Configuring region adapter mappings", LogLevel.Debug);
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

            mappings.RegisterMapping(typeof(TileLayoutControl),
                new TileLayoutControlRegionAdapter(factory));

            mappings.RegisterMapping(typeof(FlowLayoutControl),
                new FlowLayoutControlRegionAdapter(factory));

            mappings.RegisterMapping(typeof(DXTabControl),
                DevExpress.Xpf.Prism.AdapterFactory.Make<RegionAdapterBase<DXTabControl>>(factory));

            DXRegionManager.PrismVersion = PrismVersion.Prism7;
            Logger.Log("Region adapter mappings configured", LogLevel.Debug);

        }

        /// <summary>
        /// Override to configure application-wide mappings
        /// </summary>
        protected virtual void ConfigureMappings(IMapperConfigurationExpression cfg)
        {

        }
    }
}
