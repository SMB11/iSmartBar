using BusinessEntities.Products;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface IBrandRepository : IRepository<Brand, int, IBrandRepository>
    {

    }
}
