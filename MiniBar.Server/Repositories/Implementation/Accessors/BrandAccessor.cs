using BusinessEntities.Products;
using Common.DataAccess;
using Facade.Accessors;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Repositories.LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Implementation.Accessors
{
    public class BrandAccessor : AccessorBase<MiniBarDB>, IBrandAccessor
    {
        public BrandAccessor(MiniBarDB context) : base(context)
        {
        }

        [StoredProcedure("GetCategoryBrands")]
        public List<Brand> GetCategoryBrands(int categoryID)
        {
            return Context.QueryProc<Brand>("GetCategoryBrands",
                new DataParameter("categoryID", categoryID)).ToList();
        }
    }
}
