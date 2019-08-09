﻿using AutoMapper;
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
using Infrastructure.Utility;
using Infrastructure.Workitems;
using Prism.DryIoc;
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
    public abstract class PrismApplication : Prism.DryIoc.PrismApplication
    {
        protected IExceptionHandler ExceptionHandler => Container.Resolve<IExceptionHandler>();
        protected ICompositeLogger Logger => Container.Resolve<ICompositeLogger>();
        protected IUIManager UIManager => Container.Resolve<IUIManager>();

        /// <summary>
        /// Extend the DryIoc Container to support IServiceProvider interface
        /// </summary>
        /// <returns></returns>
        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        /// <summary>
        /// Contains actions that should occur last.
        /// </summary> 
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        /// <summary>
        /// Handle user-unhandled exceptions
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
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
            containerRegistry
                .RegisterSingleton(typeof(IUIManager), typeof(UIManager))
                .RegisterSingleton(typeof(IContextService), typeof(ContextService))
                .RegisterSingleton(typeof(IConnectionMonitoringService), typeof(ConnectionMonitoringService))
                .AddLogging(opt => opt.AddDebug())
                .AddDefaultExceptionHandling();
            
            // Initialize the mapper
            Mapper.Initialize(ConfigureMappings);
            Mapper.AssertConfigurationIsValid();
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

        }

        /// <summary>
        /// Override to configure application-wide mappings
        /// </summary>
        protected virtual void ConfigureMappings(IMapperConfigurationExpression cfg)
        {

        }  
    }
}
