using Infrastructure;
using Infrastructure.Framework;
using MiniBar.AnalyticsModule;
using MiniBar.AnalyticsModule.Constants;
using MiniBar.AnalyticsModule.Statistics;
using Prism.Ioc;
using Prism.Regions;
using Security;
using System.Threading.Tasks;

namespace MiniBar.Modules
{

    [SecureModule]
    public class AnalyticsModule : Module
    {

        public IRegionManager RegionManager { get; set; }
        public override string Name => "Analytics";

        public AnalyticsModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CommandManager.RegisterCommand(CommandNames.LaunchStatisticsWorkitem, new SecureAsyncCommand(LaunchStatisticsWorkitem));
        }

        private async Task LaunchStatisticsWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<StatisticsWorkitem>();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<StatisticsWorkitem>();

            RegionManager.RegisterViewWithRegion(Infrastructure.Constants.RegionNames.NavBarControlRegion, typeof(NavBarControls));
        }
    }
}
