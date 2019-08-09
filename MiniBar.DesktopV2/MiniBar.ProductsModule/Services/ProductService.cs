using Security;
using SharedEntities.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using System.Threading;

namespace MiniBar.ProductsModule.Services
{
    public class ProductService : RestConsumingServiceBase
    {
        public ProductService() : base("product")
        {
        }

        [ApiExceptionHandling]
        public Task<List<ProductDTO>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return BuildRequest().GetJsonAsync<List<ProductDTO>>(token);
        }

        [ApiExceptionHandling]
        public Task<ProductUploadDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            return BuildRequest("forupload/"+id).GetJsonAsync<ProductUploadDTO>(token);
        }

        [ApiExceptionHandling]
        public Task<int> Add(ProductUploadDTO uploadDTO)
        {

            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }


        [ApiExceptionHandling]
        public Task AddList(List<ProductUploadDTO> products)
        {

            return BuildRequest("insertMultiple").PostJsonAsync(products);
        }

        [ApiExceptionHandling]
        public async Task Update(ProductUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest(""+id).DeleteAsync().ConfigureAwait(false);
        }

    }
}
