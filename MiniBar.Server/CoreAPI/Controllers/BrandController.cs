using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facade.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Product;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ApiControllerBase
    {
        public BrandController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet("categoryBrands/{id}")]
        public async Task<List<BrandDTO>> GetCategoryBrands(int id)
        {
            IBrandManager manager = ServiceProvider.GetService<IBrandManager>();
            return await manager.GetCategoryBrands(id);
        }

        [HttpPost("insert")]
        public async Task<int> Insert(BrandDTO brand)
        {
            return await this.ServiceProvider.GetService<IBrandManager>().InsertAsync(brand);
        }


        [HttpPost("update")]
        public async Task Update(BrandDTO brand)
        {
            await this.ServiceProvider.GetService<IBrandManager>().UpdateAsync(brand);
        }

        [HttpDelete("{id}")]
        public async Task Remove(int id)
        {
            await this.ServiceProvider.GetService<IBrandManager>().RemoveAsync(id);
        }
    }
}