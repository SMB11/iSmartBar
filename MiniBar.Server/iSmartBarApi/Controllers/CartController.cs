using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Products;
using Common.Core;
using CoreAPI.Client;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.DTO.Product;

namespace iSmartBarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ApiControllerBase
    {
        public CartController(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        [HttpPost("add")]
        public async Task<ProductDTO> AddToCart(ProductCartItem cartItem)
        {
            List<ProductCartItem> cart = HttpContext.Session.Get<List<ProductCartItem>>("cart");
            ProductDTO product = await CoreAPIClient.GetProductByID(cartItem.ID);
            if (product != null)
            {
                if (cart == null)
                {
                    HttpContext.Session.Set("cart", new List<ProductCartItem> { cartItem });
                }
                else
                {
                    ProductCartItem found = cart.Find((item) => item.ID == cartItem.ID);
                    if(found != null)
                    {
                        found.Quantity += cartItem.Quantity;
                    }
                    else
                    {
                        cart.Add(cartItem);
                    }

                    HttpContext.Session.Set("cart", cart);

                }
                return product;

            }
            return null;
        }
    }
}
