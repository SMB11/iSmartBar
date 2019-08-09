using Infrastructure.Interface;
using MiniBar.Common.Workitems;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Workitems.CityManager.Views;
using MiniBar.EntityViewModels.Configuration;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CityManager
{
    public class CityManagerWorkitem : ObjectManagerWorkitem<CityManagerView, CityViewModel, CityUploadViewModel>
    {
        public CityManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "City Manager";

        protected override Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<CityUploadViewModel> details)
        {
            throw new NotImplementedException();
        }
    }
}
