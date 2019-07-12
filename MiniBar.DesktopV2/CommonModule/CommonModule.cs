using MiniBar.Common.Resources;
using MiniBar.Common.Workitems.ImportExcel;
using Prism.Ioc;
using Prism.Regions;
using Security;

namespace MiniBar.Modules
{
    public class CommonModule : Module
    {
        public override string Name => "Common";
        
        private IContainerProvider Container { get; set; }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            Container = containerProvider;
            Container.Resolve<IRegionManager>().RegisterViewWithRegion(
                Infrastructure.Constants.RegionNames.TabRegion, typeof(MainTab));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<ImportExcelWorkitem>();
        }

    }
}
