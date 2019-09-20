using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.ConfigurationModule.Services;
using MiniBar.EntityViewModels.Configuration;
using SharedEntities.DTO.Locations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Workitems.CountryManager.Services
{
    public class CountryManagerOMService : IObjectManagementService<CountryViewModel, CountryUploadViewModel>
    {
        CountryService CountryService;
        IMapper Mapper;

        public CountryManagerOMService(CountryService locationService, IMapper mapper)
        {
            CountryService = locationService;
            Mapper = mapper;
        }

        public async Task<int> Add(CountryUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {

            CountryUploadDTO dto = new CountryUploadDTO
            {
                Names = new Dictionary<string, string>(vm.Names),
            };

            return await CountryService.Add(dto);
        }

        public async Task<List<CountryViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<CountryViewModel>>(await CountryService.GetAll());
        }

        public async Task<CountryUploadViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await CountryService.GetForUploadByID(id, token);
            return new CountryUploadViewModel()
            {
                ID = dto.ID,
                Names = new EntityViewModels.Global.BindableDictionary<string>(dto.Names),
            };
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await CountryService.Remove(id);
        }

        public async Task Update(CountryUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {
            CountryUploadDTO dto = new CountryUploadDTO
            {
                ID = vm.ID,
                Names = new Dictionary<string, string>(vm.Names),
            };

            await CountryService.Update(dto);
        }
    }
}
