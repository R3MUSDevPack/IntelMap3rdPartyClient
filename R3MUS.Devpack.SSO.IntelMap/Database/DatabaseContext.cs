using R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration;
using R3MUS.Devpack.SSO.IntelMap.Entities;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<LogLine> LogLines { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<SystemGroup> SystemGroups { get; set; }
        public DbSet<Beep> Beeps { get; set; }
        public DbSet<ESIEndpoint> ESIEndpoints { get; set; }

        public DatabaseContext() : base("name=IntelDBConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogLineConfiguration());
            modelBuilder.Configurations.Add(new ChannelConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new BeepConfiguration());
            modelBuilder.Configurations.Add(new SystemGroupConfiguration());
            modelBuilder.Configurations.Add(new ESIEndpointConfiguration());
        }
    }
}