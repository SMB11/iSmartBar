using Flurl.Http;
using Security;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Services
{
    class BrandService : RestConsumingServiceBase
    {
        public BrandService() : base("brand")
        {
        }

        [ApiExceptionHandling]
        public Task<List<BrandDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<BrandDTO>>();
        }
    }
}
