using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ICountryManager
    {
        Task<List<CountryDTO>> GetAll();
        Task<CountryUploadDTO> GetForUplaodByID(int id);
        Task<int> InsertAsync(CountryUploadDTO country);
        Task UpdateAsync(CountryUploadDTO country);
        Task RemoveAsync(int id);
    }
}
