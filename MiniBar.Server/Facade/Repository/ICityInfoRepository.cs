using BusinessEntities.Location;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICityInfoRepository : ICompositeRepository<CityInfo, int, string>
    {
    }
}
