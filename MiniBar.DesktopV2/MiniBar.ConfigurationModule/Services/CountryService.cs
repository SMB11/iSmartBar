using Flurl.Http;
using Security;
using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ConfigurationModule.Services
{
    public class CountryService : RestConsumingServiceBase
    {
        public CountryService() : base("locations/countries", ConfigurationManager.AppSettings["ConfigApiUrl"])
        {   
        }


        [ApiExceptionHandling]
        public Task<List<CountryDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<CountryDTO>>();
        }

        [ApiExceptionHandling]
        public Task<CountryUploadDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            return BuildRequest("forupload/" + id).GetJsonAsync<CountryUploadDTO>(token);
        }

        [ApiExceptionHandling]
        public Task<int> Add(CountryUploadDTO uploadDTO)
        {
            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task Update(CountryUploadDTO uploadDTO)
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
