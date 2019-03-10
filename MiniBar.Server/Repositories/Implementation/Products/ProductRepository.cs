using BusinessEntities.Products;
using Facade.Repository;
using LinqToDB;
using Repositories.Base;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repositories.Implementation.Products
{
    public class ProductRepository : RepositoryBase<Product, int, ProductRepository>, IProductRepository
    {
        internal override Expression<Func<DBContext, ITable<Product>>> TableExpression => c=> c.Products;
    }
}
