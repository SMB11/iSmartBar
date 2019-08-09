using Flurl.Http;
using Security;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Services
{
    public class BrandService : RestConsumingServiceBase
    {
        public BrandService() : base("brand")
        {
        }

        [ApiExceptionHandling]
        public Task<List<BrandDTO>> GetAll()
        {
            return BuildRequest().GetJsonAsync<List<BrandDTO>>();
        }


        [ApiExceptionHandling]
        public async Task<BrandUploadDTO> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {

            BrandDTO brand = await BuildRequest("" + id).GetJsonAsync<BrandDTO>(token).ConfigureAwait(false);
            return new BrandUploadDTO { ID = brand.ID, Name = brand.Name, ImagePath = brand.ImagePath };

        }

        [ApiExceptionHandling]
        public Task<int> Add(BrandUploadDTO uploadDTO)
        {

            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task AddList(List<BrandUploadDTO> dtos)
        {
            await BuildRequest("insertMultiple").PostJsonAsync(dtos).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Update(BrandUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest("" + id).DeleteAsync().ConfigureAwait(false);
        }
    }
}
