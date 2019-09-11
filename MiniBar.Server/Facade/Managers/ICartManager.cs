using SharedEntities.DTO.Shopping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ICartManager
    {
        Task<List<CartForDay>> Validate(List<CartForDay> carts);
    }
}
