using R3MUS.Devpack.SSO.IntelMap.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            ToTable("Groups", "dbo");
            HasKey(h => h.Id);
        }
    }
}