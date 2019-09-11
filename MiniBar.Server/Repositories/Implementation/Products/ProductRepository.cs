using BusinessEntities.Products;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using Repositories.LinqToDB;
using System;
using System.Linq.Expressions;

namespace Repositories.Implementation.Products
{
    public class ProductRepository : SimpleRepositoryBase<Product, int, IProductRepository>, IProductRepository
    {
        public ProductRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Product>>> TableExpression => c => ((MiniBarDB)c).Products;
    }
}
