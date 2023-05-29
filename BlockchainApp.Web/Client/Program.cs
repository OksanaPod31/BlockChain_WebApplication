using BlockchainApp.Web.Client;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BlockchainApp.Web.Client.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddSingleton(services =>
//{
//	var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
//	var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
//	var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });
//	return new Greeter.GreeterClient(channel);
//});
builder.Services.AddAuthGrpcClient<Greeter.GreeterClient>();
builder.Services.AddAuthGrpcClient<Account.AccountClient>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthorizeApi>();


await builder.Build().RunAsync();
