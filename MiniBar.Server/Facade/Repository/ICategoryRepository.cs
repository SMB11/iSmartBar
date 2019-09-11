using BusinessEntities.Products;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICategoryRepository : IRepository<Category, int, ICategoryRepository>
    {
    }
}
