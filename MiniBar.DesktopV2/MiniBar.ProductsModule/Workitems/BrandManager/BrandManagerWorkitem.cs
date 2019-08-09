using Prism.Ioc;
using Infrastructure.Workitems;
using MiniBar.ProductsModule.Workitems.BrandManager.Views;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using System.Collections.Generic;
using DevExpress.Xpf.Ribbon;
using MiniBar.ProductsModule.Resources;
using MiniBar.ProductsModule.Workitems.BrandQC;
using SharedEntities.DTO.Product;
using System.Reactive.Linq;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Infrastructure.Interface;
using MiniBar.Common.Workitems.ObjectManager;
using Infrastructure.Office;
using Documents.Adapter;

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
