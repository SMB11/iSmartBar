using BusinessEntities.Products;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Products
{
    public class ProductInfoRepository : CompositeRepositoryBase<ProductInfo, int, string, ProductInfoRepository>, IProductInfoRepository
    {
        internal override Expression<Func<MiniBarDB, ITable<ProductInfo>>> TableExpression => c => c.ProductInfos;

        public override ProductInfo FindByID(int productID, string languageID)
        {

            using (MiniBarDB context = new MiniBarDB())
            {
                return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.ProductID == productID), context).Single();
            }
        }

        public override async Task<ProductInfo> FindByIDAsync(int productID, string languageID, CancellationToken token = default(CancellationToken))
        {

            using (MiniBarDB context = new MiniBarDB())
            {
                return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.ProductID == productID), context)).Single();
            }
        }
    }
}
