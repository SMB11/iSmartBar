using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using MiniBar.Common.MVVM;
using MiniBar.Common.Services;
using MiniBar.ProductsModule.Workitems.BrandManager.Services;
using MiniBar.Common.Workitems.ObjectManager;

namespace MiniBar.ProductsModule.Workitems.BrandManager.Views
{
    partial class BrandManagerViewModel : ObjectManagerViewModel<BrandViewModel, BrandUplaodViewModel>
    {
        BrandService BrandService { get; set; }

        IObjectManagementService<BrandViewModel, BrandUplaodViewModel> objectManagementService;
        protected override IObjectManagementService<BrandViewModel, BrandUplaodViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new BrandViewModelOMService(BrandService);
                return objectManagementService;
            }
        }

        protected override BrandUplaodViewModel CreateEmptyDetails()
        {
            return new BrandUplaodViewModel();
        }

        public BrandManagerViewModel(BrandService brandService) : base()
        {
            BrandService = brandService;
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