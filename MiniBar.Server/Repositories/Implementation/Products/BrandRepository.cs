using BusinessEntities.Products;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Products
{
    public class BrandRepository : RepositoryBase<Brand, int, IBrandRepository>, IBrandRepository
    {
        internal override Expression<Func<MiniBarDB, ITable<Brand>>> TableExpression => c => c.Brands;
        
    }
}
