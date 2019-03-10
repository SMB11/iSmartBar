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
    public class CategoryRepository : RepositoryBase<Category, int, CategoryRepository>, ICategoryRepository
    {
        internal override Expression<Func<DBContext, ITable<Category>>> TableExpression => c => c.Categories;
    }
}
