using BlockchainApp.Persistence;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Quartz;

using BlockchainApp.Web.Server;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddGrpc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlockchainDbContext>(options => options.UseSqlite(@"Data Source=Blockchain.db"), ServiceLifetime.Singleton);
builder.Services.AddSingleton<ChatRoomManager>();
builder.Services.AddQuartz(Quartz =>
{
	Quartz.UseMicrosoftDependencyInjectionJobFactory();
	var blockchainJobKey = new JobKey("BlockJob");
	Quartz.AddJob<BlockJob>(opt => opt.WithIdentity(blockchainJobKey));
	Quartz.AddTrigger(opt => opt.ForJob(blockchainJobKey).WithIdentity("BlockJob-trigger")
	.WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever()));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);



//builder.Services.AddScoped<IBlockchainDbContext, BlockchainDbContext>();
//builder.Services.AddScoped<IBlockchainDbContext>(provider => provider.GetService<BlockchainDbContext>);

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
	endpoints.MapGrpcService<ChatRoomService>().EnableGrpcWeb();
	endpoints.MapRazorPages();
	endpoints.MapControllers();
	endpoints.MapFallbackToFile("index.html");
});

//var provider = builder.Appli
//	UseScheduler(scheduler =>
//{
//	scheduler.Schedule<BlockJob>().EveryThirtySeconds();
//});
app.Run();

