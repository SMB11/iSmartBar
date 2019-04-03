using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IBrandManager
    {
        Task<List<BrandDTO>> GetAll();
        Task<BrandDTO> GetByIDAsync(int id);
        Task<List<BrandDTO>> GetCategoryBrands(int id);
        Task<int> InsertAsync(BrandDTO brand);
        Task UpdateAsync(BrandDTO brand);
        Task RemoveAsync(int id);
    }
}
