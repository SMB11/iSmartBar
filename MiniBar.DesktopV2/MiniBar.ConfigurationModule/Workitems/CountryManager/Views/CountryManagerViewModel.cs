using Infrastructure.Interface;
using Infrastructure.Workitems;
using MiniBar.Common.MVVM;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.CountryManager.Services;
using MiniBar.EntityViewModels.Configuration;
using MiniBar.EntityViewModels.Products;
using System.Collections.Generic;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Views
{
    partial class CountryManagerViewModel : ObjectManagerViewModel<CountryViewModel, CountryUploadViewModel>
    {
        private IContextService CurrentContextService;
        CountryService CountryService;

        IObjectManagementService<CountryViewModel, CountryUploadViewModel> objectManagementService;

        protected override IObjectManagementService<CountryViewModel, CountryUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new CountryManagerOMService(CountryService);
                return objectManagementService;
            }
        }

        protected override CountryUploadViewModel CreateEmptyDetails()
        {
            return new CountryUploadViewModel();
        }

        public CountryManagerViewModel(CountryService countryService, IContextService currentContextService) : base()
        {
            CurrentContextService = currentContextService;
            CountryService = countryService;
            Initialize();
        }

        protected override CountryUploadViewModel CreateCopyDetails()
        {
            return new CountryUploadViewModel() { Names = new Dictionary<string, string>(CurrentItemDetails.Names) };
        }

    }
}