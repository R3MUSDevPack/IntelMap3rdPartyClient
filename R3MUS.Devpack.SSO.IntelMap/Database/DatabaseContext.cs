using R3MUS.Devpack.SSO.IntelMap.Entities;
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

        public DatabaseContext() : base("name=IntelDBConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}