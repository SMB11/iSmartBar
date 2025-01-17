﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
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
    [Authorize]
    public class BrandController : ApiControllerBase
    {
        public BrandController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<List<BrandDTO>> GetAll()
        {
            IBrandManager manager = ServiceProvider.GetService<IBrandManager>();
            return await manager.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<BrandDTO> GetByID(int id)
        {
            IBrandManager manager = ServiceProvider.GetService<IBrandManager>();
            return await manager.GetByIDAsync(id);
        }

        [HttpGet("categoryBrands/{id}")]
        [AllowAnonymous]
        public async Task<List<BrandDTO>> GetCategoryBrands(int id)
        {
            IBrandManager manager = ServiceProvider.GetService<IBrandManager>();
            return await manager.GetCategoryBrands(id);
        }


        [HttpGet("subcategoriesBrands/{id}")]
        [AllowAnonymous]
        public async Task<Dictionary<string, List<BrandDTO>>> GetRootCategoryBrandsWithSubcategories(int id)
        {
            IBrandManager manager = ServiceProvider.GetService<IBrandManager>();
            return await manager.GetRootCategoryBrandsWithSubcategories(id);
        }

        [HttpPost("insert")]
        public async Task<int> Insert(BrandUploadDTO brand)
        {
            return await this.ServiceProvider.GetService<IBrandManager>().InsertAsync(brand);
        }
        
        [HttpPost("insertMultiple")]
        public async Task InsertMultiple(List<BrandUploadDTO> brands)
        {
            await this.ServiceProvider.GetService<IBrandManager>().InsertMultipleAsync(brands);
        }

        [HttpPost("update")]
        public async Task Update(BrandUploadDTO brand)
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