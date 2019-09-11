using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Statistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ApiControllerBase
    {
        public VisitController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<List<VisitDTO>> GetAll()
        {

            return await ServiceProvider.GetService<IVisitManager>().GetAllAsync();
        }

        [HttpPost("add")]
        public async Task<int> Insert(VisitDTO visit)
        {
            return await ServiceProvider.GetService<IVisitManager>().InsertAsync(visit);
        }
    }
}
