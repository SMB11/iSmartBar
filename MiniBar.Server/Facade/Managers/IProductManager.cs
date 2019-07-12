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
        Task<List<ProductDTO>> GetTopFive();
        Task<ProductDTO> GetByID(int id);
        Task<List<ProductDTO>> GetBrandProducts(int id);
        Task<List<ProductDTO>> GetCategoryBrandProducts(int brandID, int categoryID);
        Task<int> InsertAsync(ProductUploadDTO product);
        Task InsertMultipleAsync(List<ProductUploadDTO> products);
        Task UpdateAsync(ProductUploadDTO product);
        Task RemoveAsync(int id);
    }
}
