using FastReport.DataVisualization.Charting;
using FluentMigrator.Runner;
using HamrahanSystem.Application.DependencyInjection;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Infrastructure.DependencyInjection;
using HamrahanSystem.Infrastructure.Repository;
using HamrahanSystem.Presntation;
using HamrahanSystem.Presntation.Middleware;
using HamrahanSystem.Presntation.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var demoMode = config.GetValue<bool>("DemoMode:Enabled");
var useRedis = config.GetValue("Infrastructure:UseRedis", true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

if (!demoMode)
{
	builder.Services.AddDbContext<AdelModel>(options => options.UseSqlServer(config.GetConnectionString("Default")));
	builder.Services.AddSingleton<IDbConnection>(sp =>
	{
		var connectionString = builder.Configuration.GetConnectionString("Default");
		return new SqlConnection(connectionString);
	});
	builder.Services.AddInfrastructureService(config);
	builder.Services.AddApplicationService(config);
	builder.Services.AddScoped<IAuditContext, AuditContext>();
	builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
	builder.Services.AddSingleton<ICustomLensPrintSettingsService, CustomLensPrintSettingsService>();
	builder.Services.AddScoped<ICustomLensAutoPrintService, CustomLensAutoPrintService>();
	builder.Services.AddHttpClient("CustomLensAutoPrint", client =>
	{
		client.Timeout = TimeSpan.FromSeconds(10);
	});
	if (useRedis)
	{
		builder.Services.AddDistributedRedisCache(options =>
		{
			options.Configuration = config.GetConnectionString("ServerRedis");
			options.InstanceName = "";
		});
	}
}

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt16(config.GetConnectionString("CacheRedisMinute") ?? "60"));
	options.Cookie.Name = config.GetConnectionString("CookiName") ?? "HamrahanSystem";
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
	options.LoginPath = "/Account/Login";
	options.LogoutPath = "/Account/Logout";
	options.AccessDeniedPath = "/Home/Index/424";
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt16(config.GetConnectionString("CacheRedisMinute") ?? "60"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

if (!demoMode)
{
	builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
	builder.Services.AddFastReport();
	builder.Services.AddFluentMigratorCore()
	  .ConfigureRunner(rb => rb
		.AddSqlServer()
		.WithGlobalConnectionString(config.GetConnectionString("Default"))
		.ScanIn(typeof(Program).Assembly).For.Migrations())
		.AddLogging(lb => lb.AddFluentMigratorConsole());
}

var app = builder.Build();

var supportedCultures = new[]
{
	new CultureInfo("en-US"),
	new CultureInfo("fa-IR")
};

var localizationOptions = new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("en-US"),
	SupportedCultures = supportedCultures,
	SupportedUICultures = supportedCultures
};

localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
{
	new QueryStringRequestCultureProvider(),
	new CookieRequestCultureProvider(),
	new AcceptLanguageHeaderRequestCultureProvider()
};

localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
localizationOptions.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

if (!demoMode)
{
	app.UseAuthentication();
	app.UseAuthorization();
	app.UseMiddleware<AuditMiddleware>();
	app.UseFastReport();

	using var scope = app.Services.CreateScope();
	var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
	try
	{
		runner.MigrateUp();
	}
	catch (Exception ex)
	{
		throw new Exception(ex.ToString());
	}
}

if (demoMode)
{
	app.MapControllerRoute(
		name: "demo-root",
		pattern: "",
		defaults: new { controller = "Demo", action = "Index" });

	app.MapControllerRoute(
		name: "demo-pages",
		pattern: "demo/{action=Workspace}/{id?}",
		defaults: new { controller = "Demo" });
}
else
{
	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
}

app.Run();
