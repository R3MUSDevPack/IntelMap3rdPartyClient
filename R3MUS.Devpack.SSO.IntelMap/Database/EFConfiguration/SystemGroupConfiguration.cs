using R3MUS.Devpack.SSO.IntelMap.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class SystemGroupConfiguration : EntityTypeConfiguration<SystemGroup>
    {
        public SystemGroupConfiguration()
        {
            ToTable("SystemGroups", "dbo");

            HasKey(h => h.GroupId);
            HasKey(h => h.SystemName);
        }
    }
}