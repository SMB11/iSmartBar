using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IProductManager
    {
        Task<ProductUploadDTO> GetForUplaodByID(int id);
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> GetByID(int id);
        Task<List<ProductDTO>> GetBrandProducts(int id);
        Task<int> InsertAsync(ProductUploadDTO product);
        Task UpdateAsync(ProductUploadDTO product);
        Task RemoveAsync(int id);
    }
}
