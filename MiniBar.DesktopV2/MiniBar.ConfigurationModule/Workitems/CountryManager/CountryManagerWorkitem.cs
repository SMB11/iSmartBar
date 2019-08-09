using Infrastructure.Interface;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Workitems.CountryManager.Views;
using MiniBar.EntityViewModels.Configuration;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager
{
    [SingleInstanceWorkitem]
    public class CountryManagerWorkitem : ObjectManagerWorkitem<CountryManagerView, CountryViewModel, CountryUploadViewModel>
    {
        public CountryManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Country Manager";

        protected override Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<CountryUploadViewModel> details)
        {
            throw new NotImplementedException();
        }
    }
}
