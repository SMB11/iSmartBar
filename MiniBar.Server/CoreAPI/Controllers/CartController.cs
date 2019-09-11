using BusinessEntities.Products;
using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Product;
using SharedEntities.DTO.Shopping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ApiControllerBase
    {
        public CartController(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        [HttpPost("validate")]
        public async Task<List<CartForDay>> Validate(List<CartForDay> carts)
        {
            return await ServiceProvider.GetService<ICartManager>().Validate(carts);
            
        }
    }
}
