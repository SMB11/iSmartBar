using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.ConfigurationModule.Services;
using MiniBar.EntityViewModels.Configuration;
using SharedEntities.DTO.Locations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.HotelManager.Services
{
    public class HotelManagerOMService : IObjectManagementService<HotelViewModel, HotelUploadViewModel>
    {
        HotelService HotelService;
        IMapper Mapper;

        public HotelManagerOMService(HotelService locationService, IMapper mapper)
        {
            HotelService = locationService;
            Mapper = mapper;
        }

        public async Task<int> Add(HotelUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {

            HotelDTO dto = new HotelDTO
            {
                Name = vm.Name,
                CityID = vm.CityID
            };

            return await HotelService.Add(dto);
        }

        public async Task<List<HotelViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<HotelViewModel>>(await HotelService.GetAll());
        }

        public async Task<HotelUploadViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await HotelService.GetForUploadByID(id, token);
            return new HotelUploadViewModel()
            {
                ID = dto.ID,
                CityID = dto.CityID,
                Name = dto.Name
            };
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await HotelService.Remove(id);
        }

        public async Task Update(HotelUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {
            HotelDTO dto = new HotelDTO
            {
                ID = vm.ID,
                Name = vm.Name,
                CityID = vm.CityID
            };

            await HotelService.Update(dto);
        }
    }
}
