using Security;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace MiniBar.ProductsModule.Services
{
    public class ProductService : RestConsumingServiceBase
    {
        public ProductService() : base("product")
        {
        }

        [ApiExceptionHandling]
        public Task<List<ProductDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<ProductDTO>>();
        }

        [ApiExceptionHandling]
        public Task<ProductUploadDTO> GetForUploadByID(int id)
        {

            return BuildRequest("forupload/"+id).GetJsonAsync<ProductUploadDTO>();
        }

        [ApiExceptionHandling]
        public Task<int> Add(ProductUploadDTO uploadDTO)
        {

            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task Update(ProductUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest(""+id).DeleteAsync();
        }

    }
}
