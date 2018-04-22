using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class LogLineConfiguration : EntityTypeConfiguration<LogLine>
    {
        public LogLineConfiguration()
        {
            ToTable("LogLines");
            HasKey(k => new { k.LogDateTime, k.UserName });
        }
    }
}