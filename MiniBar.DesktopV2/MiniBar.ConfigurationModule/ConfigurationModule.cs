using Infrastructure;
using Infrastructure.Framework;
using MiniBar.ConfigurationModule;
using MiniBar.ConfigurationModule.Constants;
using MiniBar.ConfigurationModule.Resources;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.CityManager;
using MiniBar.ConfigurationModule.Workitems.CountryManager;
using MiniBar.ConfigurationModule.Workitems.HotelManager;
using Prism.Ioc;
using Prism.Regions;
using Security;
using System.Threading.Tasks;

namespace MiniBar.Modules
{
    public class ConfigurationModule : Module
    {
        public override string Name => "Configuration";

        public IRegionManager RegionManager { get; set; }

        public ConfigurationModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CommandManager.RegisterCommand(CommandNames.OpenCountryManager, new SecureAsyncCommand(OpenCountryManager));
            CommandManager.RegisterCommand(CommandNames.OpenCountryManagerModal, new SecureAsyncCommand(OpenCountryManagerModal));
            CommandManager.RegisterCommand(CommandNames.OpenCityManager, new SecureAsyncCommand(OpenCityManager));
            CommandManager.RegisterCommand(CommandNames.OpenCityManagerModal, new SecureAsyncCommand(OpenCityManagerModal));
            CommandManager.RegisterCommand(CommandNames.OpenHotelManager, new SecureAsyncCommand(OpenHotelManager));
            CommandManager.RegisterCommand(CommandNames.OpenHotelManagerModal, new SecureAsyncCommand(OpenHotelManagerModal));

        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<CountryService>();
            containerRegistry.RegisterSingleton<CityService>();
            containerRegistry.RegisterSingleton<HotelService>();

            containerRegistry.Register<CountryManagerWorkitem>();
            containerRegistry.Register<CityManagerWorkitem>();
            containerRegistry.Register<HotelManagerWorkitem>();

            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.NavBarControlRegion, typeof(NavBarControls));
            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.MainPageMenuRegion, typeof(MainPageMenuGroup));

        }

        private Task OpenCountryManager()
        {
            return CurrentContextService.LaunchWorkItem<CountryManagerWorkitem>();
        }

        private Task OpenCountryManagerModal()
        {
            return CurrentContextService.LaunchModalWorkItem<CountryManagerWorkitem>();
        }

        private Task OpenHotelManager()
        {
            return CurrentContextService.LaunchWorkItem<HotelManagerWorkitem>();
        }

        private Task OpenHotelManagerModal()
        {
            return CurrentContextService.LaunchModalWorkItem<HotelManagerWorkitem>();
        }

        private Task OpenCityManager()
        {
            return CurrentContextService.LaunchWorkItem<CityManagerWorkitem>();
        }

        private Task OpenCityManagerModal()
        {
            return CurrentContextService.LaunchModalWorkItem<CityManagerWorkitem>();
        }
    }
}
