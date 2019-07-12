using Infrastructure.Interface;
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
    public class LoginWorkitem : ModalWorkitem
    {
        public LoginWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override Size Size => new Size(300, 300);

        public override string WorkItemName => "Login";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            object view = new LoginView();
            container.Register(view);
            Popup.SetContent(view);
        }
    }
}
