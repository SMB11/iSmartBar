using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.ConfigurationModule.Services;
using MiniBar.EntityViewModels.Configuration;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CityManager.Services
{
    public class CityManagerOMService : IObjectManagementService<CityViewModel, CityUploadViewModel>
    {
        CityService CityService;
        IMapper Mapper;

        public CityManagerOMService(CityService locationService, IMapper mapper)
        {
            CityService = locationService;
            Mapper = mapper;
        }

        public async Task<int> Add(CityUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {

            CityUploadDTO dto = new CityUploadDTO
            {
                Names = new Dictionary<string, string>(vm.Names),
            };

            return await CityService.Add(dto);
        }

        public async Task<List<CityViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<CityViewModel>>(await CityService.GetAll());
        }

        public async Task<CityUploadViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await CityService.GetForUploadByID(id, token);
            return new CityUploadViewModel()
            {
                ID = dto.ID,
                Names = new EntityViewModels.Global.BindableDictionary<string>(dto.Names),
                CountryID = dto.CountryID
            };
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await CityService.Remove(id);
        }

        public async Task Update(CityUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {
            CityUploadDTO dto = new CityUploadDTO
            {
                ID = vm.ID,
                Names = new Dictionary<string, string>(vm.Names),
                CountryID = vm.CountryID
            };

            await CityService.Update(dto);
        }
    }
}
