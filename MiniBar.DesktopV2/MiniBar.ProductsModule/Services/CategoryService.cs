using Flurl.Http;
using Security;
using SharedEntities.DTO.Global;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Services
{
    public class CategoryService : RestConsumingServiceBase
    {
        public CategoryService() : base("category")
        {
        }

        [ApiExceptionHandling]
        public Task<List<CategoryDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<CategoryDTO>>();
        }

        [ApiExceptionHandling]
        public Task<CategoryUploadDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            return BuildRequest("forupload/" + id).GetJsonAsync<CategoryUploadDTO>(token);
        }

        [ApiExceptionHandling]
        public Task<int> Add(CategoryUploadDTO uploadDTO)
        {
            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }
        
        [ApiExceptionHandling]
        public async Task Update(CategoryUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest("" + id).DeleteAsync().ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public Task InsertChanges(List<CategoryUploadDTO> newItems, List<CategoryUploadDTO> oldItems, List<CategoryUploadDTO> changedItems)
        {
            return BuildRequest("insertChanges").PostJsonAsync(new ListChanges<CategoryUploadDTO>(newItems, oldItems, changedItems));
        }
    }
}
