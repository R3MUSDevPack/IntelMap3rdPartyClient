using R3MUS.Devpack.SSO.IntelMap.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class ChannelConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelConfiguration()
        {
            ToTable("Channels", "dbo");
            HasKey(w => w.Id);
            Property(w => w.Name).HasColumnName("Channel");
        }
    }
}