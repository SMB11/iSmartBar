using BusinessEntities.Security;
using Common.Core;
using Facade.Managers;
using Facade.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Managers.Implementation
{
    public class UserRolesManager : IUserRolesManager
    {
        
        private IServiceProvider serviceProvider;
        private UserManager<User> _userManager;
        private RoleManager<LinqToDB.Identity.IdentityRole> _roleManager;

        public UserRolesManager(IServiceProvider provider, UserManager<User> userManager, RoleManager<LinqToDB.Identity.IdentityRole> roleManager)
        {
            serviceProvider = provider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserRolesDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            List<User> users = await serviceProvider.GetService<IUserRepository>().GetAllAsync();
            List<UserRolesDTO> userRoles = new List<UserRolesDTO>();
            foreach(User user in users) {
                UserRolesDTO userRole = new UserRolesDTO();
                userRole.UserID = user.Id;
                userRole.UserName = user.UserName;
                userRole.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                userRoles.Add(userRole);
            }
            return userRoles;
        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task SaveList(List<UserRolesDTO> list, CancellationToken token = default(CancellationToken))
        {
            foreach(UserRolesDTO userRolesDTO in list)
            {
                User user = await serviceProvider.GetService<IUserRepository>().GetByIDAsync(userRolesDTO.UserID);
                List<LinqToDB.Identity.IdentityRole> roles = new List<LinqToDB.Identity.IdentityRole>();
                foreach (var roleName in await _userManager.GetRolesAsync(user))
                {
                    if (!userRolesDTO.Roles.Contains(roleName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, roleName);
                    }
                    userRolesDTO.Roles.Remove(roleName);
                }
                foreach(var role in userRolesDTO.Roles)
                {

                    await _userManager.AddToRoleAsync(user, role);
                }

            }
        }
    }
}
