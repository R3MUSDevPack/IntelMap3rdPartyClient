using R3MUS.Devpack.SSO.IntelMap.Entities;
using System.Linq;

namespace R3MUS.Devpack.SSO.IntelMap.Database.Repositories
{
    public class ESIEndpointRepository : IESIEndpointRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ESIEndpointRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ESIEndpoint GetEndpoint()
        {
            return _databaseContext.ESIEndpoints.First();
        }
    }
}