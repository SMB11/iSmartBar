using DevExpress.Xpf.Core;
using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Workitems;
using Prism.Ioc;
using Security.Workitems.Login.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Security.Workitems.Login
{
    public class LoginWorkitem : Workitem
    {
        public LoginWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(300, 300), ResizeMode.NoResize, WindowStartupLocation.CenterOwner, false, true));
        }

        public override string WorkItemName => "Login";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new LoginView(), ScreenRegion.Content);
        }
    }
}
