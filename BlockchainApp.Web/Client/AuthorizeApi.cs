using Blazored.LocalStorage;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlockchainApp.Web.Client
{
    public class AuthorizeApi
    {
        private readonly ILocalStorageService _localStorageService;
        private IdentityAuthenticationStateProvider stateProvide;
        private readonly Task<Account.AccountClient> accountClient;
        public AuthorizeApi(AuthenticationStateProvider stateProvider, ILocalStorageService localStorageService, Task<Account.AccountClient> accountClient)
        {
            this.stateProvide = (IdentityAuthenticationStateProvider)stateProvider;
            _localStorageService = localStorageService;
            this.accountClient = accountClient;
        }
        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var tokenResponse = await (await accountClient).LoginAsync(new LoginRequest()
                {
                    Login = username,
                    Password = password
                });

                if (tokenResponse.ResultCase == LoginResponse.ResultOneofCase.Info)
                {
                    var token = tokenResponse.Info.Token;
                    Console.WriteLine(token);
                    await _localStorageService.SetItemAsync("token", token);
                    stateProvide.MarkUserAsAuthenticated(token);

                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public async Task<bool> Register(string username, string password)
        {
            try
            {
                var tokenResponse = await (await accountClient).RegisterAsync(new RegisterRequest()
                {
                    Login = username,
                    Password = password
                });

                if(tokenResponse.ResultCase == LoginResponse.ResultOneofCase.Info)
                {
                    var token = tokenResponse.Info.Token;
                    await _localStorageService.SetItemAsync("token", token);
                    stateProvide.MarkUserAsAuthenticated(token);
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

    }
}
