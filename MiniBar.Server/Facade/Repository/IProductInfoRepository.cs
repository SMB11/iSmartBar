using BusinessEntities.Products;
using Common.DataAccess;

namespace Facade.Repository
{
    public interface IProductInfoRepository : ICompositeRepository<ProductInfo, int, string>
    {
    }
}
