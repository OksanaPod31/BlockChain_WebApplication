using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlockchainApp.Web.Client
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;

        public IdentityAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorageService.GetItemAsStringAsync("token");
            if(!string.IsNullOrEmpty(token))
            {
                var authUser = new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>() { new Claim(ClaimTypes.Name, "admin") }, "jwt"));
                return new AuthenticationState(authUser);
            }
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public void MarkUserAsAuthenticated(string token)
        {

            var authUser = new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>() { new Claim(ClaimTypes.Name, "admin") }, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
