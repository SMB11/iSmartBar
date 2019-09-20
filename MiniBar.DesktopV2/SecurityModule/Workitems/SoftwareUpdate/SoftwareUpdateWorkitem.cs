using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Workitems;
using Prism.Ioc;
using Security.Workitems.SoftwareUpdate.Views;
using System.Windows;

namespace Security.Workitems.SoftwareUpdate
{
    public class SoftwareUpdateWorkitem : Workitem
    {
        public SoftwareUpdateWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(400, 180), ResizeMode.NoResize, WindowStartupLocation.CenterOwner, false));
        }

        public override string WorkItemName => "Software Update";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new UpdateView(), ScreenRegion.Content);
        }
    }
}
