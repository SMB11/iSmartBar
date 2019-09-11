using BusinessEntities.Statistics;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface IVisitRepository : IRepository<Visit, int, IVisitRepository>
    {
    }
}
