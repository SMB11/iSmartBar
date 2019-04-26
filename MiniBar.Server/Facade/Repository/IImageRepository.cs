using BusinessEntities.Global;

namespace Facade.Repository
{
    public interface IImageRepository : IRepository<Image, int, IImageRepository>
    {
    }
}
