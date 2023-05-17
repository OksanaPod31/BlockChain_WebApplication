using BlazorAppEmpty.Web;

//using BlockchainApp.Protos;
using Grpc.Core;
using Grpc.Net.Client;
//using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddSingleton(services =>
//{
//    var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
//    ChannelBase channel = GrpcChannel.ForAddress(baseUri,
//        new GrpcChannelOptions
//        {
//            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
//        });
//    return new Greeter.GreeterClient(channel);
//});

await builder.Build().RunAsync();
