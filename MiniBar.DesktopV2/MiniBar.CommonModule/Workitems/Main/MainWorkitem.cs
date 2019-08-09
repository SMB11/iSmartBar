using Infrastructure.Interface;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.Main.Views;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.Common.Workitems.Main
{
    public class MainWorkitem : NullWorkitem
    {
        public MainWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Main";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new MainTab(), Infrastructure.Modularity.ScreenRegion.Content);
        }
    }
}
