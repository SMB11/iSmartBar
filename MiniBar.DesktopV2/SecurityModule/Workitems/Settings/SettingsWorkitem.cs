using Infrastructure.Workitems;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Workitems.Settings
{
    class SettingsWorkitem : Workitem
    {
        public SettingsWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Settings";

    }
}
