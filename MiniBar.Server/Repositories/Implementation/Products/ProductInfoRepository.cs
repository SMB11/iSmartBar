using BusinessEntities.Products;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using Repositories.LinqToDB;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Products
{
    public class ProductInfoRepository : CompositeRepositoryBase<ProductInfo, int, string, ProductInfoRepository>, IProductInfoRepository
    {
        public ProductInfoRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<ProductInfo>>> TableExpression => c => ((MiniBarDB)c).ProductInfos;

        public override ProductInfo FindByID(int productID, string languageID)
        {
            return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.ProductID == productID), Context).Single();
        }

        public override async Task<ProductInfo> FindByIDAsync(int productID, string languageID, CancellationToken token = default(CancellationToken))
        {
            return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.ProductID == productID), Context)).Single();
        }
    }
}
