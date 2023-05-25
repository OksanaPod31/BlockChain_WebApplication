using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlockchainApp.Web.Client
{
    public class AuthorizeApi
    {
        private readonly ILocalStorageService _localStorageService;
        private IdentityAuthenticationStateProvider stateProvide;
        public AuthorizeApi(AuthenticationStateProvider stateProvider, ILocalStorageService localStorageService)
        {
            this.stateProvide = (IdentityAuthenticationStateProvider)stateProvider;
            _localStorageService = localStorageService;
        }
        public async Task<bool> Login(string username, string password)
        {
            var token = "jdhgchjea67";
            await _localStorageService.SetItemAsync("token", token);
            stateProvide.MarkUserAsAuthenticated(token);
            return true;
        }
    }
}
