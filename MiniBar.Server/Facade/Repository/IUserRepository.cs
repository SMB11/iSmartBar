using BusinessEntities.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facade.Repository
{
    public interface IUserRepository
    {

        Task<List<User>> GetAllAsync();
        Task<User> GetByIDAsync(string ID);
    }
}
