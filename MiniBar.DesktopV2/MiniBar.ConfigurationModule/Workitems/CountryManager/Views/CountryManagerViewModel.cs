using AutoMapper;
using Infrastructure.Interface;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.CountryManager.Services;
using MiniBar.EntityViewModels.Configuration;
using System.Collections.Generic;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Views
{
    partial class CountryManagerViewModel : ObjectManagerViewModel<CountryViewModel, CountryUploadViewModel>
    {
        private IContextService CurrentContextService;
        CountryService CountryService;
        IMapper Mapper;

        IObjectManagementService<CountryViewModel, CountryUploadViewModel> objectManagementService;

        protected override IObjectManagementService<CountryViewModel, CountryUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new CountryManagerOMService(CountryService, Mapper);
                return objectManagementService;
            }
        }

        protected override CountryUploadViewModel CreateEmptyDetails()
        {
            return new CountryUploadViewModel();
        }

        public CountryManagerViewModel(CountryService countryService, IContextService currentContextService, IMapper mapper) : base()
        {
            CurrentContextService = currentContextService;
            CountryService = countryService;
            Mapper = mapper;
            Initialize();
        }

        protected override CountryUploadViewModel CreateCopyDetails()
        {
            return new CountryUploadViewModel() { Names = new Dictionary<string, string>(CurrentItemDetails.Names) };
        }

    }
}