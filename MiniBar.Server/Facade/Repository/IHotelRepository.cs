using BusinessEntities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repository
{
    public interface IHotelRepository : IRepository<Hotel, int, IHotelRepository>
    {
        List<HotelWithCity> GetHotelWithCities(string languageID);
    }
}
