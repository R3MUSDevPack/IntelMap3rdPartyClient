using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap.Services
{
    public interface IEveAuthenticationService
    {
        ClaimsIdentity GenerateIdentity();
        ClaimsIdentity GenerateIdentity(string authToken);
        string GetAuthToken(Uri requestUri);
        ActionResult SSOLogin();
    }
}