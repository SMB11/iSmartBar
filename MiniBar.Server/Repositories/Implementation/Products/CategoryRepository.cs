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
    public class CategoryRepository : SimpleRepositoryBase<Category, int, ICategoryRepository>, ICategoryRepository
    {
        public CategoryRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Category>>> TableExpression => c => ((MiniBarDB)c).Categories;
    }
}
