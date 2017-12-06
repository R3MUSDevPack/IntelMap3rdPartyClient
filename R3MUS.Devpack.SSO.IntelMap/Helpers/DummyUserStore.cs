using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R3MUS.Devpack.SSO.IntelMap.Helpers
{
    public class DummyUserStore<T> : IUserStore<T>,
        IUserRoleStore<T> where T : SSOApplicationUser
    {
        private readonly EveAuthenticationService _authService;

        public DummyUserStore()
        {
            _authService = new EveAuthenticationService();
        }

        public Task AddToRoleAsync(T user, string roleName)
        {
            user.Roles.Add(new IdentityUserRole()
            {
                RoleId = roleName
            });
            return Task.FromResult(true);
        }

        public Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(T user)
        {
            return Task.FromResult<IList<string>>(user.Roles.Select(s => s.RoleId).ToList());
        }

        public Task<bool> IsInRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }
    }
}