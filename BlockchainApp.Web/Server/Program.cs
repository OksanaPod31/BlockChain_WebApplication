using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddGrpc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseGrpcWeb();


app.MapRazorPages();
//app.MapControllers();
//app.MapFallbackToFile("index.html");
app.UseEndpoints(endpoints =>
{
	// map to and register the gRPC service
	endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
	endpoints.MapRazorPages();
	endpoints.MapControllers();
	endpoints.MapFallbackToFile("index.html");
});

app.Run();