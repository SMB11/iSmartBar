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
    public class BrandRepository : SimpleRepositoryBase<Brand, int, IBrandRepository>, IBrandRepository
    {
        public BrandRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Brand>>> TableExpression => c => ((MiniBarDB)c).Brands;
    }
}
