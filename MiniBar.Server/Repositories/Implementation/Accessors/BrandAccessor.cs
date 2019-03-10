using BusinessEntities.Products;
using Facade.Accessors;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Implementation.Accessors
{
    public class BrandAccessor : IBrandAccessor
    {
        [StoredProcedure("GetCategoryBrands")]
        public List<Brand> GetCategoryBrands(int categoryID)
        {
            using(DBContext context = new DBContext())
            {
                return context.QueryProc<Brand>("GetCategoryBrands",
                   new DataParameter("categoryID", categoryID)).ToList();
            }
        }
    }
}
