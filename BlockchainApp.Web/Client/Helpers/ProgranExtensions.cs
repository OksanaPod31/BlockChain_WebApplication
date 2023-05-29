using Blazored.LocalStorage;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;

namespace BlockchainApp.Web.Client.Helpers
{
    public static class ProgranExtensions
    {
        public static void AddAuthGrpcClient<T>(this IServiceCollection services) where T : ClientBase
        {
            services.AddScoped(async provider =>
            {
                var client = (T)Activator.CreateInstance(typeof(T), await GetChannel(provider));

                return client;
            });
        }
        private static async Task<GrpcChannel> GetChannel(IServiceProvider provider)
        {
            var nav = provider.GetService<NavigationManager>();
            var storage = provider.GetService<ILocalStorageService>();

            string token = await storage.GetItemAsStringAsync("token");

            return nav.GetAuthChannel(token);
        }
    }
}
