using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Repository
{
    public interface ICategoryNameRepository : ICompositeRepository<CategoryName, int, string>
    {
    }
}
