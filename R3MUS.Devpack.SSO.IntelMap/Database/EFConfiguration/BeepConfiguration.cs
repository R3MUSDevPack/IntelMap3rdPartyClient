using R3MUS.Devpack.SSO.IntelMap.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class BeepConfiguration : EntityTypeConfiguration<Beep>
    {
        public BeepConfiguration()
        {
            ToTable("Beeps", "dbo");
            HasKey(h => h.Id);

            HasMany<SystemGroup>(h => h.SystemGroups)
                .WithOptional()
                .HasForeignKey(f => f.BeepId);
        }
    }
}