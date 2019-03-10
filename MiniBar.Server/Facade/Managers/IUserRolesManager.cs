using SharedEntities.DTO.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IUserRolesManager
    {
        Task<IEnumerable<UserRolesDTO>> GetAllAsync(CancellationToken token = new CancellationToken());

        Task SaveList(List<UserRolesDTO> list, CancellationToken token = new CancellationToken());
    }
}
