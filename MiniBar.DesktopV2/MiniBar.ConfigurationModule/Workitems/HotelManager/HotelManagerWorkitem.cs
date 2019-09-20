using DevExpress.Xpf.Ribbon;
using Documents.Adapter;
using Documents.Editors;
using Infrastructure.Interface;
using Infrastructure.Office;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Resources;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.HotelManager.Views;
using MiniBar.ConfigurationModule.Workitems.HotelQC;
using MiniBar.EntityViewModels.Configuration;
using Prism.Ioc;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.HotelManager
{
    public class HotelManagerWorkitem : ObjectManagerWorkitem<HotelManagerView, HotelViewModel, HotelUploadViewModel>
    {
        HotelService HotelService;

        public HotelManagerWorkitem(IContainerExtension container, HotelService hotelService) : base(container)
        {
            HotelService = hotelService;
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            Resource("Cities", () => ((HotelManagerViewModel)ObjectManagerViewModel).Cities);
        }

        public override string WorkItemName => "Hotel Manager";

        protected override Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<HotelUploadViewModel> details)
        {
            return CurrentContextService.LaunchModalWorkItem<HotelQCWorkitem>(details, this);
        }

        protected override ExcelDocument<HotelUploadViewModel> GetDocument()
        {
            HotelManagerViewModel hotelManagerViewModel = (ObjectManagerViewModel as HotelManagerViewModel);
            DocumentAdapter<HotelUploadViewModel> documentAdapter = new DocumentAdapter<HotelUploadViewModel>();
            documentAdapter.Column(
                b => b.Name,
                "Name");

            documentAdapter.Column(
                p => p.CityID,
                p => p.City,
                "City",
                new CellListEditor<CityViewModel>(
                        hotelManagerViewModel.Cities,
                        c => c.ID,
                        c => c.Name
                    )
                );
            return new ExcelDocument<HotelUploadViewModel>(documentAdapter);
        }

        protected override IObservable<System.Reactive.Unit> AddList(List<HotelUploadViewModel> list)
        {

            List<HotelDTO> dtos = new List<HotelDTO>();

            foreach (var vm in list)
            {
                HotelDTO dto = new HotelDTO
                {
                    ID = vm.ID,
                    Name = vm.Name,
                    CityID = vm.CityID
                };
                dtos.Add(dto);
            }

            return Observable.FromAsync(() => HotelService.AddList(dtos));
        }

        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new HotelManagerRibbonCategory();
        }
    }
}
