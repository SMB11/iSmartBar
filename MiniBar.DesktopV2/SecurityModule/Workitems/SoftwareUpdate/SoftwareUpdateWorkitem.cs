using Infrastructure.Interface;
using Infrastructure.Workitems;
using Prism.Ioc;
using Security.Workitems.SoftwareUpdate.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Security.Workitems.SoftwareUpdate
{
    public class SoftwareUpdateWorkitem : ModalWorkitem
    {
        public SoftwareUpdateWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override Size Size => new Size(400, 200);

        public override ResizeMode ResizeMode => ResizeMode.NoResize;

        public override string WorkItemName => "Software Update";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            Popup.SetContent(container.Register(new UpdateView()));
        }
    }
}
