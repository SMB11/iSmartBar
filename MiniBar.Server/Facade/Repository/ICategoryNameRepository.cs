using BusinessEntities.Products;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface ICategoryNameRepository : ICompositeRepository<CategoryName, int, string>
    {
    }
}
