using Flurl.Http;
using Security;
using SharedEntities.DTO.Statistics;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniBar.AnalyticsModule.Services
{
    public class StatisticsService : RestConsumingServiceBase
    {

        public StatisticsService() : base("visit", ConfigurationManager.AppSettings["ProductApiUrl"])
        {
        }

        [ApiExceptionHandling]
        public Task<List<VisitDTO>> GetVisits()
        {
            return BuildRequest().GetJsonAsync<List<VisitDTO>>();
        }
    }
}
