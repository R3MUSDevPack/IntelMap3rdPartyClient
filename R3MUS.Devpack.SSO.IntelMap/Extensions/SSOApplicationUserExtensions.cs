using R3MUS.Devpack.SSO.IntelMap.Database;
using R3MUS.Devpack.SSO.IntelMap.Entities;
using R3MUS.Devpack.SSO.IntelMap.Enums;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Extensions
{
    public static class SSOApplicationUserExtensions
    {
        public static void GenerateUser(this SSOApplicationUser self)
        {
            var group = new Group();
            var corporation = new ESI.Models.Corporation.Detail(self.CorporationId);
            if (corporation.Alliance_Id.HasValue)
            {
                self.AllianceId = corporation.Alliance_Id;
            }
            using (var context = new DatabaseContext())
            {
                if (context.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Corporation && w.Id == self.CorporationId))
                {
                    group = context.Groups.First(w1 =>
                        w1.Id == context.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Corporation && w.Id == self.CorporationId).GroupId);
                }
                else if (self.AllianceId.HasValue && context.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Alliance && w.Id == self.AllianceId))
                {
                    group = context.Groups.First(w1 =>
                        w1.Id == context.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Alliance && w.Id == self.AllianceId).GroupId);
                }
            }
            self.GroupId = (int)group.Id;
            self.GroupName = group.Name;
        }
    }
}