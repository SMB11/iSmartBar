using Flurl.Http;
using Security;
using SharedEntities.DTO.Locations;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Services
{
    public class CityService : RestConsumingServiceBase
    {
        public CityService() : base("locations/cities", ConfigurationManager.AppSettings["ProductApiUrl"])
        {
        }


        [ApiExceptionHandling]
        public Task<List<CityDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<CityDTO>>();
        }

        [ApiExceptionHandling]
        public Task<CityUploadDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            return BuildRequest("forupload/" + id).GetJsonAsync<CityUploadDTO>(token);
        }

        [ApiExceptionHandling]
        public Task<int> Add(CityUploadDTO uploadDTO)
        {
            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task Update(CityUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest("" + id).DeleteAsync().ConfigureAwait(false);
        }

    }
}
