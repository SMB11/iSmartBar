using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Accessors
{
    public interface IBrandAccessor
    {
        List<Brand> GetCategoryBrands(int categoryID);
    }
}
