using Microsoft.AspNet.Identity;
using R3MUS.Devpack.ESI;
using R3MUS.Devpack.SSO.IntelMap.Database;
using R3MUS.Devpack.SSO.IntelMap.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap.Services
{
    public class EveAuthenticationService : IEveAuthenticationService
    {
        private readonly IESIEndpointRepository _esiEndpointRepository;

        public EveAuthenticationService(IESIEndpointRepository esiEndpointRepository)
        {
            _esiEndpointRepository = esiEndpointRepository;
        }

        public ActionResult SSOLogin()
        {
            var endpoint = _esiEndpointRepository.GetEndpoint();

            return SingleSignOn.SignOn(endpoint.CallbackUrl, endpoint.ClientId,
                new List<string>());
        }

        public string GetAuthToken(Uri requestUri)
        {
            var endpoint = _esiEndpointRepository.GetEndpoint();

            return SingleSignOn.GetTokensFromAuthenticationToken(endpoint.ClientId,
                endpoint.SecretKey, SingleSignOn.GetAuthorisationCode(requestUri)).AccessToken;
        }

        public ClaimsIdentity GenerateIdentity()
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            
            return identity;
        }

        public ClaimsIdentity GenerateIdentity(string authToken)
        {
            var toon = new ESI.Models.Character.Detail(authToken);
            var corp = new ESI.Models.Corporation.Detail(toon.CorporationId);

            using(var context = new DatabaseContext())
            {
                if (!context.Corporations.Any(s => s.Id == corp.Id) 
                    && (!corp.AllianceId.HasValue || !context.Alliances.Any(s => s.Id == corp.AllianceId)))
                {
                    return null;
                }
            }

            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, toon.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));

            identity.AddClaim(new Claim(ClaimTypes.Name, toon.Name));

            return identity;
        }
    }
}