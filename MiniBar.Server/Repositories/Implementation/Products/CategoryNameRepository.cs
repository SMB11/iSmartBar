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
    public class CategoryNameRepository : CompositeRepositoryBase<CategoryName, int, string, CategoryNameRepository>, ICategoryNameRepository
    {
        public CategoryNameRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<CategoryName>>> TableExpression => c => ((MiniBarDB)c).CategoryNames;

        public override CategoryName FindByID(int categoryID, string languageID)
        {
            return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CategoryID == categoryID), Context).Single();
        }

        public override async Task<CategoryName> FindByIDAsync(int categoryID, string languageID, CancellationToken token = default(CancellationToken))
        {
            return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CategoryID == categoryID), Context)).Single();
        }

    }
}
