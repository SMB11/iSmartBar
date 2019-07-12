using SharedEntities.DTO.Updates;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Flurl.Http;
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
            var a = await BuildRequest("check/" + major).GetJsonAsync<List<AssemblyDTO>>();
            return a;
        }
        
    }
}
