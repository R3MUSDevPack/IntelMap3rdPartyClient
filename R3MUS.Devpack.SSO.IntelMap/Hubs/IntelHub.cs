using Microsoft.AspNet.SignalR;
using R3MUS.Devpack.SSO.IntelMap.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.SSO.IntelMap.Services;

namespace R3MUS.Devpack.SSO.IntelMap.Hubs
{
    public partial class IntelHub : Hub
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISSOUserService _ssoUserService;

        public IntelHub()
        {
            _databaseContext = new DatabaseContext();
            _ssoUserService = new SSOUserService();
        }

        public override Task OnConnected()
        {
            JoinGroup();

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            JoinGroup();

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            LeaveGroup();

            return base.OnDisconnected(stopCalled);
        }

        private string GetGroupName()
        {
            var claims = ((ClaimsIdentity)Context.User.Identity).Claims;
            if (claims.Any(w => w.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid"))
            {
                return claims.First(w => w.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid").Value;
            } 
            else
            {
                try
                {
                    return _ssoUserService.CreateUser(Context.User.Identity.Name.GetCharacterIdByName().ToString())
                        .GroupName;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}