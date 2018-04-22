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

            using (var context = new DatabaseContext())
            {
                if (context.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Corporation && w.EntityId == self.CorporationId))
                {
                    group.Id = context.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Corporation
                        && w.EntityId == self.CorporationId).GroupId;
                }
                else if (self.AllianceId.HasValue && context.GroupMemberships.Any(w => w.EntityTypeId == (int)EntityType.Alliance && w.EntityId == self.AllianceId))
                {
                    group.Id = context.GroupMemberships.First(w => w.EntityTypeId == (int)EntityType.Alliance
                        && w.EntityId == self.AllianceId).GroupId;
                }
                group = context.Groups.First(w => w.Id == group.Id && !w.Disabled);
            }
            self.GroupId = group.Id;
            self.GroupName = group.Name;
            self.DefaultRegion = group.DefaultRegion;
        }
    }
}