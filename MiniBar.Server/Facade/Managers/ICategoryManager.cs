using BusinessEntities.Products;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ICategoryManager
    {
        Task<List<CategoryDTO>> GetAllAsync();
        Task<List<CategoryDTO>> GetRootCategoriesAsync();
        Task<List<CategoryDTO>> GetSubcategoriesAsync(int? id);
        Task<int> InsertAsync(CategoryUploadDTO category);
        Task UpdateAsync(CategoryUploadDTO category);
        Task RemoveAsync(int id);
    }
}
