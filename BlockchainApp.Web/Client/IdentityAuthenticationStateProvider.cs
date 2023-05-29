using Blazored.LocalStorage;
using BlockchainApp.Web.Client.Helpers;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Security.Claims;

namespace BlockchainApp.Web.Client
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly Task<Account.AccountClient> accountClient;

        public IdentityAuthenticationStateProvider(ILocalStorageService localStorageService, Task<Account.AccountClient> accountClient)
        {
            this.localStorageService = localStorageService;
            
            this.accountClient = accountClient;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorageService.GetItemAsStringAsync("token");
            if(!string.IsNullOrEmpty(token))
            {
                try
                {
                    var authUser = await (await accountClient).GetUserProfileAsync(new UserInfoRequest());
                    if (authUser.ResultCase == UserInfoResponse.ResultOneofCase.Profile)
                        return Jwt.GetStateFromJwt(token);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex);
                } 
               
            }
            return new (new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public void MarkUserAsAuthenticated(string token)
        {

            var authState = Task.FromResult(Jwt.GetStateFromJwt(token));
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
