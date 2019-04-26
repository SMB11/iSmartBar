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


        [ApiExceptionHandling]
        public async Task<BrandUploadDTO> GetForUploadByID(int id)
        {

            BrandDTO brand = await BuildRequest("" + id).GetJsonAsync<BrandDTO>();
            return new BrandUploadDTO { ID = brand.ID, Name = brand.Name, ImagePath = brand.ImagePath };

        }

        [ApiExceptionHandling]
        public Task<int> Add(BrandUploadDTO uploadDTO)
        {

            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task Update(BrandUploadDTO uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO);
        }

        [ApiExceptionHandling]
        public async Task Remove(int id)
        {
            await BuildRequest("" + id).DeleteAsync();
        }
    }
}
