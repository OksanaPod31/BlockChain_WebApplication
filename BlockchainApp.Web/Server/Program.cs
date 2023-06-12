using BlockchainApp.Persistence;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Quartz;

using BlockchainApp.Web.Server;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.ComponentModel.DataAnnotations;
using BlockchainApp.Domain.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BlockchainApp.Web.Server.Services;
using BlockchainApp.Crypto.Signature;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddGrpc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlockchainDbContext>(options => options.UseSqlite(@"Data Source=Blockchain.db"), ServiceLifetime.Singleton);
//builder.Services.AddDbContext<ServerKeysContext>(options => options.UseSqlite(@"Data Source=BlockchainKeys.db"), ServiceLifetime.Singleton);
builder.Services.AddIdentity<ChatUser, IdentityRole>()
	.AddEntityFrameworkStores<BlockchainDbContext>()
	.AddDefaultTokenProviders();
TokenParameters tokenParameters = new();
builder.Services.AddSingleton(tokenParameters);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = true;
		//options.SecurityTokenValidators.Add(new )
	});
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 3;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
	builder.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader()
	.WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddAuthorization();
//TokenParameters tokenParameters = new();
builder.Services.AddSingleton<ChatRoomManager>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SignatureService>();
builder.Services.AddMemoryCache();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<HttpContextService>();

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
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllers();
//app.MapFallbackToFile("index.html");
app.UseEndpoints(endpoints =>
{
	// map to and register the gRPC service
	endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
    endpoints.MapGrpcService<AccountService>().EnableGrpcWeb();

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

