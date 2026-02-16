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
using System.Net.WebSockets;
using System.Reflection;




var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AdelModel>(options => options.UseSqlServer(config.GetConnectionString("Default")));
builder.Services.AddSingleton<IDbConnection>(sp =>
{
	var connectionString = builder.Configuration.GetConnectionString("Default");
	return new SqlConnection(connectionString);
});
builder.Services.AddMvc();
builder.Services.AddInfrastructureService(config);
builder.Services.AddApplicationService(config);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuditContext, AuditContext>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<ICustomLensPrintSettingsService, CustomLensPrintSettingsService>();
builder.Services.AddScoped<ICustomLensAutoPrintService, CustomLensAutoPrintService>();
builder.Services.AddHttpClient("CustomLensAutoPrint", client =>
{
	client.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddDistributedRedisCache(options => {
	options.Configuration = config.GetConnectionString("ServerRedis");
	options.InstanceName = "";
});





builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt16(config.GetConnectionString("CacheRedisMinute")));
	options.Cookie.Name = config.GetConnectionString("CookiName");
});



builder.Services.AddAuthentication(options =>
{

	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	
})
.AddCookie(options =>
{
	options.LoginPath = "/Account/Login"; // Customize login page path
	options.LogoutPath = "/Account/Logout";
	options.AccessDeniedPath = "/Home/Index/424"; // Access denied page
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt16(config.GetConnectionString("CacheRedisMinute"))); // Cookie expiration
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddFastReport();
builder.Services.AddFluentMigratorCore()
  .ConfigureRunner(rb => rb
	.AddSqlServer()
	.WithGlobalConnectionString(config.GetConnectionString("Default"))
	.ScanIn(typeof(Program).Assembly).For.Migrations())
	.AddLogging(lb => lb.AddFluentMigratorConsole());


var app = builder.Build();

#region roh added
// ✅ مرحله 1: تعریف زبان‌های پشتیبانی‌شده
var supportedCultures = new[]
{
	new CultureInfo("en-US"),
	new CultureInfo("fa-IR")
};

// ✅ مرحله 2: تنظیم پیش‌فرض‌ها
var localizationOptions = new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("en-US"),
	SupportedCultures = supportedCultures,
	SupportedUICultures = supportedCultures
};

// ✅ مرحله 3: تنظیم ترتیب اولویت تشخیص Culture
// به ترتیب از QueryString → Cookie → Accept-Language مرورگر

// ابتدا لیست جدیدی می‌سازیم تا null نباشد
localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
{
	new QueryStringRequestCultureProvider(),
	new CookieRequestCultureProvider(),
	new AcceptLanguageHeaderRequestCultureProvider()
};

// جایگذاری در ابتدای لیست تا QueryString اولویت داشته باشد
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
localizationOptions.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
// AcceptLanguageHeader هم ممکن است از قبل در لیست باشد، اگر نه اضافه‌اش کن

// ✅ مرحله 4: فعال‌سازی Middleware
app.UseRequestLocalization(localizationOptions);

#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuditMiddleware>();
app.UseFastReport();

using (var scope = app.Services.CreateScope())
{
	var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
	try
	{
		runner.MigrateUp();
	} // Apply all pending migrations
	catch(Exception ex)
	{ 
		throw new Exception(ex.ToString());
	}
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
