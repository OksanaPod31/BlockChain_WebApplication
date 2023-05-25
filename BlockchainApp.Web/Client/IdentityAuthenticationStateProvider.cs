using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlockchainApp.Web.Client
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            return new AuthenticationState(MarkUserAsAuthenticated("jhj"));
        }
        public ClaimsPrincipal MarkUserAsAuthenticated(string token)
        {
            if(string.IsNullOrEmpty(token)) { return new ClaimsPrincipal(); }
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>() { new Claim(ClaimTypes.Name, "admin") }, "jwt"));


        }
    }
}
