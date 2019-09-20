using Infrastructure.Interface;
using Infrastructure.Workitems;
using MiniBar.AnalyticsModule.Statistics.Views;
using Prism.Ioc;

namespace MiniBar.AnalyticsModule.Statistics
{
    public class StatisticsWorkitem : Workitem
    {
        public StatisticsWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Dashboard";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new StatisticsView(), Infrastructure.Modularity.ScreenRegion.Content);
        }

    }
}
