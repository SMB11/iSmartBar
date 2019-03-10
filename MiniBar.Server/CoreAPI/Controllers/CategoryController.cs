using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Culture;
using Common.ResponseHandling;
using Facade.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Product;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        public CategoryController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet("root")]
        public async Task<List<CategoryDTO>> GetRootCategories()
        {
            return await this.ServiceProvider.GetService<ICategoryManager>().GetRootCategoriesAsync();
        }

        [HttpGet("sub/{id}")]
        public async Task<List<CategoryDTO>> GetSubcategories(int id)
        {
            return await this.ServiceProvider.GetService<ICategoryManager>().GetSubcategoriesAsync(id);
        }

        [HttpPost("insert")]
        public async Task<int> Insert(CategoryUploadDTO category)
        {
            return await this.ServiceProvider.GetService<ICategoryManager>().InsertAsync(category);
        }


        [HttpPost("update")]
        public async Task Update(CategoryUploadDTO category)
        {
            await this.ServiceProvider.GetService<ICategoryManager>().UpdateAsync(category);
        }

        [HttpDelete("{id}")]
        public async Task Remove(int id)
        {
            await this.ServiceProvider.GetService<ICategoryManager>().RemoveAsync(id);
        }
    }
}