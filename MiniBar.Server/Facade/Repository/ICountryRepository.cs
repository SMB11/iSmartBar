using BusinessEntities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repository
{
    public interface ICountryRepository : IRepository<Country, int, ICountryRepository>
    {
    }
}
