using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using MiniBar.ProductsModule.Workitems.BrandManager.Services;

namespace MiniBar.ProductsModule.Workitems.BrandManager.Views
{
    partial class BrandManagerViewModel : ObjectManagerViewModel<BrandViewModel, BrandUplaodViewModel>
    {
        BrandService BrandService { get; set; }
        IMapper Mapper;

        IObjectManagementService<BrandViewModel, BrandUplaodViewModel> objectManagementService;
        protected override IObjectManagementService<BrandViewModel, BrandUplaodViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new BrandViewModelOMService(BrandService, Mapper);
                return objectManagementService;
            }
        }

        protected override BrandUplaodViewModel CreateEmptyDetails()
        {
            return new BrandUplaodViewModel();
        }

        public BrandManagerViewModel(BrandService brandService, IMapper mapper) : base()
        {
            BrandService = brandService;
            Mapper = mapper;
            Initialize();
        }

        protected override BrandUplaodViewModel CreateCopyDetails()
        {
            return new BrandUplaodViewModel()
            {
                Name = CurrentItemDetails.Name
            };
        }

    }
}