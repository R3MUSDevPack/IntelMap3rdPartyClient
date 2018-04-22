using R3MUS.Devpack.SSO.IntelMap.Extensions;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Services
{
    public class SSOUserService : ISSOUserService
    {
        public SSOApplicationUser CreateUser(string userId)
        {
            var toon = new ESI.Models.Character.Detail(Convert.ToInt64(userId));

            var siteUser = new SSOApplicationUser()
            {
                Id = userId,
                UserName = toon.Name,
                CorporationId = toon.CorporationId,
                AllianceId = toon.AllianceId
            };
            siteUser.GenerateUser();
            return siteUser;
        }
    }
}