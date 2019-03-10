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
    public class CategoryNameRepository : CompositeRepositoryBase<CategoryName, int, string, CategoryNameRepository>, ICategoryNameRepository
    {
        internal override Expression<Func<DBContext, ITable<CategoryName>>> TableExpression => c => c.CategoryNames;

        public override CategoryName FindByID(int categoryID, string languageID)
        {

            using (DBContext context = new DBContext())
            {
                return ExecuteSelect(t => t.Where(e => e.LanguageID == languageID && e.CategoryID == categoryID), context).Single();
            }
        }

        public override async Task<CategoryName> FindByIDAsync(int categoryID, string languageID, CancellationToken token = default(CancellationToken))
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelectAsync(t => t.Where(e => e.LanguageID == languageID && e.CategoryID == categoryID), context)).Single();
            }
        }
        
    }
}
