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
    class CategoryService : RestConsumingServiceBase
    {
        public CategoryService() : base("category")
        {
        }

        [ApiExceptionHandling]
        public Task<List<CategoryDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<CategoryDTO>>();
        }
    }
}
