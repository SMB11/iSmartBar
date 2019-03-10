using BusinessEntities.Security;
using Facade.Repository;
using Repositories.LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Implementation.Security
{
    public class UserRepository : IUserRepository
    {

        public async Task<List<User>> GetAllAsync()
        {
            using(DBContext context = new DBContext())
            {

                return await Task.Run(() => context.Users.ToList());
            }
        }

        public async Task<User> GetByIDAsync(string id)
        {
            using (DBContext context = new DBContext())
            {

                return await Task.Run(() => context.Users.Where(u => u.Id == id).First());
            }
        }
    }
}
