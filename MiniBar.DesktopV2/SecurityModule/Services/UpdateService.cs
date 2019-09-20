using Flurl.Http;
using SharedEntities.DTO.Updates;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Security.Services
{
    class UpdateService : RestConsumingServiceBase
    {
        public UpdateService() : base("updates", ConfigurationManager.AppSettings["UpdateApiUrl"])
        {

        }

        public async Task<List<AssemblyDTO>> GetLatestFileInfo(int major)
        {
            var a = await BuildRequest("check/" + major).GetJsonAsync<List<AssemblyDTO>>().ConfigureAwait(false);
            return a;
        }

    }
}
