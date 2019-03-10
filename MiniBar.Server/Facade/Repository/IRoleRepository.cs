using System.Threading.Tasks;

namespace Facade.Repository
{
    public interface IRoleRepository
    {
        Task<LinqToDB.Identity.IdentityRole> GetByIDAsync(string ID);
    }
}
