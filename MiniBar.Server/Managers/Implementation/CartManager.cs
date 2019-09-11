using Facade.Managers;
using SharedEntities.DTO.Shopping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Product;

namespace Managers.Implementation
{
    public class CartManager : ICartManager
    {
        IServiceProvider _serviceProvider;

        public CartManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<CartForDay>> Validate(List<CartForDay> carts)
        {
            IProductManager productManager = _serviceProvider.GetService<IProductManager>();
            foreach (var cart in carts)
            {
                if (cart != null)
                {
                    foreach (var section in cart)
                    {
                        if (section != null)
                        {
                            foreach (var product in section)
                            {
                                if (product != null)
                                {
                                    ProductDTO real = await productManager.GetByID(product.ID);
                                    if (real.Price != product.Price)
                                        product.Price = real.Price;
                                }
                            }
                        }
                    }
                }
            }
            return carts;
        }
    }
}
