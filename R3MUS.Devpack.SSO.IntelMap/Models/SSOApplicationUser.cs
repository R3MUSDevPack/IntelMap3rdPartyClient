using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Security.Principal;

namespace R3MUS.Devpack.SSO.IntelMap.Models
{
    public class SSOApplicationUser : IdentityUser, IPrincipal
    {
        public string AuthToken { get; set; }
        public long CorporationId { get; set; }
        public long? AllianceId { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public string DefaultRegion { get; set; }

        public IIdentity Identity { get; private set; }

        public void AddToRole(string roleName)
        {
            Roles.Add(new IdentityUserRole() { RoleId = roleName, UserId = this.Id });
        }

        public bool IsInRole(string role)
        {
            return Roles.Select(s => s.RoleId).Contains(role);
        }
    }
}