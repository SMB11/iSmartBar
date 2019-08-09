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
    public class HotelService : RestConsumingServiceBase
    {
        public HotelService() : base("locations/hotels", ConfigurationManager.AppSettings["ConfigApiUrl"])
        {
        }


        [ApiExceptionHandling]
        public Task<List<HotelDetailedDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<HotelDetailedDTO>>();
        }

        [ApiExceptionHandling]
        public Task<HotelDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            return BuildRequest("byid/" + id).GetJsonAsync<HotelDTO>(token);
        }

        [ApiExceptionHandling]
        public Task<int> Add(HotelDTO uploadDTO)
        {
            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }


        [ApiExceptionHandling]
        public async Task AddList(List<HotelDTO> hotels)
        {
            await BuildRequest("insertlist").PostJsonAsync(hotels).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Update(HotelDTO uploadDTO)
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
