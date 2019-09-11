using BusinessEntities.Location;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICountryRepository : IRepository<Country, int, ICountryRepository>
    {
    }
}
