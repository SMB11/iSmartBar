﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.DTO.Product;
using Facade.Managers;
using Microsoft.AspNetCore.Authorization;
using Common.Core;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        public ProductController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ProductDTO> GetByID(int id)
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetByID(id);
        }

        [HttpGet("brand/{id}")]
        [AllowAnonymous]
        public async Task<List<ProductDTO>> GetBrandProducts(int id)
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetBrandProducts(id);
        }


        [HttpGet("category/{categoryID}/brand/{brandID}")]
        [AllowAnonymous]
        public async Task<List<ProductDTO>> GetCategoryBrandProducts(int brandID, int categoryID)
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetCategoryBrandProducts(brandID, categoryID);
        }

        [HttpGet("forupload/{id}")]
        [AllowAnonymous]
        public async Task<ProductUploadDTO> GetForUplaodByID(int id)
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetForUplaodByID(id);
        }


        [HttpGet]
        public async Task<List<ProductDTO>> GetAll()
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetAll();
        }


        [HttpGet("topFive")]
        [AllowAnonymous]
        public async Task<List<ProductDTO>> GetTopFive()
        {
            return await this.ServiceProvider.GetService<IProductManager>().GetTopFive();
        }

        [HttpPost("insert")]
        public async Task<int> Insert(ProductUploadDTO product)
        {
            return await this.ServiceProvider.GetService<IProductManager>().InsertAsync(product);
        }
        
        [HttpPost("insertMultiple")]
        public async Task Insert(List<ProductUploadDTO> products)
        {
            await this.ServiceProvider.GetService<IProductManager>().InsertMultipleAsync(products);
        }

        [HttpPost("update")]
        public async Task Update(ProductUploadDTO product)
        {
            await this.ServiceProvider.GetService<IProductManager>().UpdateAsync(product);
        }

        [HttpDelete("{id}")]
        public async Task Remove(int id)
        {
            await this.ServiceProvider.GetService<IProductManager>().RemoveAsync(id);
        }
    }
}