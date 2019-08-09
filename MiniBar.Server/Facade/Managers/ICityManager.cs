using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ICityManager
    {
        Task<List<CityDTO>> GetAll();
        Task<List<CityDTO>> GetByCountryID(int countryID);
        Task<CityUploadDTO> GetForUplaodByID(int id);
        Task<int> InsertAsync(CityUploadDTO city);
        Task UpdateAsync(CityUploadDTO city);
        Task RemoveAsync(int id);
    }
}
