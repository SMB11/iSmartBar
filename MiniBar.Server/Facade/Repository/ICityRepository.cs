using BusinessEntities.Location;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICityRepository : IRepository<City, int, ICityRepository>
    {
    }
}
