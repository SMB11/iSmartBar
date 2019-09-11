using BusinessEntities.Products;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface IProductRepository : IRepository<Product, int, IProductRepository>
    {
    }
}
