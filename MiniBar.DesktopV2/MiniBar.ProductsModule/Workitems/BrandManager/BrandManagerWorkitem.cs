using DevExpress.Xpf.Ribbon;
using Documents.Adapter;
using Infrastructure.Interface;
using Infrastructure.Office;
using Infrastructure.Workitems;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Resources;
using MiniBar.ProductsModule.Services;
using MiniBar.ProductsModule.Workitems.BrandManager.Views;
using MiniBar.ProductsModule.Workitems.BrandQC;
using Prism.Ioc;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.BrandManager
{
    public class BrandManagerWorkitem : ObjectManagerWorkitem<BrandManagerView, BrandViewModel, BrandUplaodViewModel>
    {

        public static WorkitemMetadata Metadata = new WorkitemMetadata("Brand Manager", "Add/Edit/Remove Brands");

        #region Constructor

        public BrandManagerWorkitem(IContainerExtension container, BrandService brandService) : base(container)
        {
            BrandService = brandService;
        }


        #endregion

        #region Properties
        public override string WorkItemName => Metadata.Name;

        protected override string ImportTemplateName
        {
            get
            {
                return "BrandImportTemplate";
            }
        }

        public BrandService BrandService { get; set; }
        #endregion


        protected override ExcelDocument<BrandUplaodViewModel> GetDocument()
        {
            BrandManagerViewModel brandManagerViewModel = (ObjectManagerViewModel as BrandManagerViewModel);
            DocumentAdapter<BrandUplaodViewModel> documentAdapter = new DocumentAdapter<BrandUplaodViewModel>();
            documentAdapter.Column(
                b => b.Name,
                "Name");
            return new ExcelDocument<BrandUplaodViewModel>(documentAdapter);
        }

        protected override Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<BrandUplaodViewModel> res)
        {
            return CurrentContextService.LaunchModalWorkItem<BrandQCWorkitem>(res, this);
        }

        protected override IObservable<Unit> AddList(List<BrandUplaodViewModel> list)
        {
            List<BrandUploadDTO> dtos = new List<BrandUploadDTO>();

            foreach (var vm in list)
            {
                BrandUploadDTO dto = new BrandUploadDTO
                {
                    ID = vm.ID,
                    Name = vm.Name
                };
                dtos.Add(dto);
            }

            return Observable.FromAsync(() => BrandService.AddList(dtos));

        }

        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new BrandManagerRibbonCategory();
        }

    }
}
