using accountslogin.Data;

using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
//using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.logging;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.IO; // For Path.Combine

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configure Identity
// I use my existing account table so I need to remove the configuration related to ASP.NET Core Identity and ensure my custom authentication system is properly set up:
/*
builder.Services.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings, lockout settings, etc.
});
*/


// Configure services by calling the extension method
builder.Services.AddProjectServices();
/*
builder.Services.AddSingleton<ApplicationProperties>();
builder.Services.AddScoped<AuthenticationManager>();
*/


builder.Services.AddHttpContextAccessor(); // Ensure HttpContextAccessor is available for AccountManager


// Configure MVC and Razor
// Configure the application to use views files from the "views" folder instead of "Views"
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    // Clearing existing locations to set custom paths
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/src/main/webapp/WEB_INF/views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/src/main/webapp/WEB_INF/views/Shared/{0}.cshtml");

    // Adding View Component Location Formats
    options.ViewLocationFormats.Add("/src/main/webapp/WEB_INF/views/Shared/components/{0}/{1}.cshtml");


    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/src/main/webapp/WEB_INF/areas/{2}/views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/src/main/webapp/WEB_INF/areas/{2}/views/Shared/{0}.cshtml");

    // Adding Area View Component Location Formats
    options.AreaViewLocationFormats.Add("/src/main/webapp/WEB_INF/areas/{2}/views/Shared/components/{0}/{1}.cshtml");


});

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.SlidingExpiration = true;
});





// Test the database connection before configuring services
if (!DatabaseConnectionTester.TestConnectionString(connectionString, builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>(), DbType.MSSQL))
{
    // If the connection test fails, you might want to exit or handle it differently
    // For now, let's just log and continue for demonstration purposes
    Console.WriteLine("Exiting due to database connection failure.");
    return;
}
else
{
    Console.WriteLine("Database connection success.");
}

// Logging
builder.Services.AddLogging(builder => builder
    .AddConsole()
    .AddDebug()
    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));


var app = builder.Build();


// Configure LogManager here
//LogManager.Configure(app.Services);


// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// Configure the application to use static files from the "static" folder instead of "wwwroot"
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "src", "main", "resources", "static")),
    RequestPath = ""
});


app.UseHttpsRedirection();
app.UseStaticFiles();
// Enable Session
// Make sure you call this before calling app.UseMvc() or app.UseRouting()
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Index}/{action=Index}/{id?}");
app.Run();