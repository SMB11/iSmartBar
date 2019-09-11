using BusinessEntities.Security;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Identity;
using Repositories.LinqToDB;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Implementation.Security
{
    public class RoleRepository : RepositoryBase<Role, RoleRepository>, IRoleRepository
    {
        public RoleRepository(MiniBarDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Role>>> TableExpression => c => ((MiniBarDB)c).Roles;

        public async Task<IdentityRole> GetByIDAsync(string ID)
        {
            return (await ExecuteSelectAsync(t => t.Where(r => r.Id == ID), Context)).First();
        }
    }
}
