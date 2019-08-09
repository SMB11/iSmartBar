using AutoMapper;
using Infrastructure.Interface;
using Infrastructure.Utility;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.CityManager.Services;
using MiniBar.EntityViewModels.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CityManager.Views
{
    partial class CityManagerViewModel : ObjectManagerViewModel<CityViewModel, CityUploadViewModel>
    {

        CityService CityService;
        IContextService CurrentContextService;
        CountryService CountryService;

        IObjectManagementService<CityViewModel, CityUploadViewModel> objectManagementService;

        protected override IObjectManagementService<CityViewModel, CityUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new CityManagerOMService(CityService);
                return objectManagementService;
            }
        }

        private List<CountryViewModel> countries;
        public List<CountryViewModel> Countries
        {
            get { return countries; }
            set
            {
                SetProperty(ref countries, value, nameof(Countries));
            }
        }

        protected override CityUploadViewModel CreateEmptyDetails()
        {
            return new CityUploadViewModel();
        }

        public CityManagerViewModel(CityService cityService, CountryService countryService, IContextService currentContextService) : base()
        {
            CurrentContextService = currentContextService;
            CountryService = countryService;
            CityService = cityService;
            Initialize();
        }

        protected override CityUploadViewModel CreateCopyDetails()
        {
            return new CityUploadViewModel() { Names = new Dictionary<string, string>(CurrentItemDetails.Names) };
        }


        protected override async void Initialize()
        {

            IsListLoading = true;
            IsObjectLoading = true;

            await Task.WhenAll(
                LoadList(),
                LoadCountries()
                );

            IsListLoading = false;
            IsObjectLoading = false;
        }


        protected override async Task RefreshList()
        {
            if (Countries == null)
            {

                IsListLoading = true;
                IsObjectLoading = true;

                await Task.WhenAll(
                    LoadCountries(),
                    base.RefreshList()
                    );

                IsListLoading = false;
                IsObjectLoading = false;
            }
            else
                await base.RefreshList();
        }


        private async Task LoadCountries()
        {
            try
            {
                Countries = Mapper.Map<List<CountryViewModel>>((await CountryService.GetAll()));

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }
    }
}