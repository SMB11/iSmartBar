using SharedEntities.DTO.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IHotelManager
    {
        Task<List<HotelDTO>> GetAll(int cityID);
    }
}
