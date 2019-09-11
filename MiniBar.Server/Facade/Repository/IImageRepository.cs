using BusinessEntities.Global;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface IImageRepository : IRepository<Image, int, IImageRepository>
    {
    }
}
