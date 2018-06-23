using R3MUS.Devpack.SSO.IntelMap.Entities;
using System.Data.Entity.ModelConfiguration;

namespace R3MUS.Devpack.SSO.IntelMap.Database.EFConfiguration
{
    public class ESIEndpointConfiguration : EntityTypeConfiguration<ESIEndpoint>
    {
        public ESIEndpointConfiguration()
        {
            ToTable("ESIEndpoint", "dbo");

            HasKey(h => h.Id);
        }
    }
}