using R3MUS.Devpack.SSO.IntelMap.Models;

namespace R3MUS.Devpack.SSO.IntelMap.Services
{
    public interface ISSOUserService
    {
        SSOApplicationUser CreateUser(string userId);
    }
}