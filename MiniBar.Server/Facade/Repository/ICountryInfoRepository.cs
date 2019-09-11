using BusinessEntities.Location;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICountryInfoRepository : ICompositeRepository<CountryInfo, int, string>
    {
    }
}
