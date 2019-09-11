using BusinessEntities.Location;
using Common.DataAccess;
using System.Collections.Generic;

namespace Facade.Repository
{
    public interface IHotelRepository : IRepository<Hotel, int, IHotelRepository>
    {
        List<HotelWithCity> GetHotelWithCities(string languageID);
    }
}
