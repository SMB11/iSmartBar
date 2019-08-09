using MiniBar.Common.Workitems.EntityQC;
using MiniBar.ConfigurationModule.Workitems.HotelQC.Views;
using MiniBar.EntityViewModels.Configuration;
using Prism.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.ConfigurationModule.Workitems.HotelQC
{
    class HotelQCWorkitem : EntityQCWorkitem<HotelUploadViewModel>
    {
        public HotelQCWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Hotel Quality Control";

        protected override FrameworkElement CreateView()
        {
            return new Views.HotelQCView(this);
        }


        private HotelQCViewModel HotelQCViewModel
        {
            get
            {
                return QCViewModel as HotelQCViewModel;
            }
        }


        protected override void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
            object root = Parent.RequestResource("Cities");
            if (root != null && root is IEnumerable<CityViewModel>)
                HotelQCViewModel.Cities = new ObservableCollection<CityViewModel>((IEnumerable<CityViewModel>)root);
        }
    }
}
