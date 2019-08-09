using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IHotelManager
    {
        Task<List<HotelDTO>> GetByCityID(int cityID);

        List<HotelDetailedDTO> GetAll();

        Task<HotelDTO> GetByID(int id);
        
        Task<int> InsertAsync(HotelDTO Hotel);

        Task InsertMultipleAsync(List<HotelDTO> hotels);

        Task UpdateAsync(HotelDTO Hotel);
        
        Task RemoveAsync(int id);
        
    }
}
