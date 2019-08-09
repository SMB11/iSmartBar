using AutoMapper;
using Infrastructure.Utility;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.ConfigurationModule.Services;
using MiniBar.ConfigurationModule.Workitems.HotelManager.Services;
using MiniBar.EntityViewModels.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.HotelManager.Views
{
    partial class HotelManagerViewModel : ObjectManagerViewModel<HotelViewModel, HotelUploadViewModel>
    {

        HotelService HotelService;
        CityService CityService;

        IObjectManagementService<HotelViewModel, HotelUploadViewModel> objectManagementService;

        protected override IObjectManagementService<HotelViewModel, HotelUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new HotelManagerOMService(HotelService);
                return objectManagementService;
            }
        }


        private List<CityViewModel> cities;
        public List<CityViewModel> Cities
        {
            get { return cities; }
            set
            {
                SetProperty(ref cities, value, nameof(Cities));
            }
        }

        protected override HotelUploadViewModel CreateEmptyDetails()
        {
            return new HotelUploadViewModel();
        }

        public HotelManagerViewModel(HotelService hotelService, CityService cityService) : base()
        {
            HotelService = hotelService;
            CityService = cityService;
            Initialize();
        }

        protected override HotelUploadViewModel CreateCopyDetails()
        {
            return new HotelUploadViewModel() { City = CurrentItemDetails.City, CityID = CurrentItemDetails.CityID, Name = CurrentItemDetails.Name };
        }


        protected override async void Initialize()
        {

            IsListLoading = true;
            IsObjectLoading = true;

            await Task.WhenAll(
                LoadList(),
                LoadCities()
                );

            IsListLoading = false;
            IsObjectLoading = false;
        }


        protected override async Task RefreshList()
        {
            if (Cities == null)
            {
                IsListLoading = true;
                IsObjectLoading = true;

                await Task.WhenAll(
                    LoadCities(),
                    base.RefreshList()
                    );

                IsListLoading = false;
                IsObjectLoading = false;
            }
            else
                await base.RefreshList();
        }


        private async Task LoadCities()
        {
            try
            {
                Cities = Mapper.Map<List<CityViewModel>>((await CityService.GetAll()));

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

    }
}