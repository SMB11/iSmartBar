using Facade.Repository;
using LinqToDB;
using LinqToDB.Identity;
using Repositories.LinqToDB;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Implementation.Security
{
    public class RoleRepository : IRoleRepository
    {
        public async Task<IdentityRole> GetByIDAsync(string ID)
        {
            using(MiniBarDB context = new MiniBarDB())
            {
                return await context.Roles.Where(role => role.Id == ID).FirstAsync();
            }
        }
    }
}
