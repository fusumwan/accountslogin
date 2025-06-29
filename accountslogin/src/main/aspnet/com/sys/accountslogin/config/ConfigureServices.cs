using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using Microsoft.Extensions.DependencyInjection;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.config
{
    public static class ConfigureServices
    {
        public static void AddProjectServices(this IServiceCollection services)
        {

            // Configure logging if not already configured in Program.cs
            /*
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
            });
            */
            // Add services here
            services.AddSingleton<ApplicationProperties>();
            services.AddSingleton<JwtUtil>();
            services.AddScoped<SessionManager>();
            services.AddScoped<AuthenticationManager>();
            services.AddScoped<IAccountService, AccountServiceImpl>();  // Ensure this is registered
            services.AddScoped<CustomUserDetailsService>();
            // Add more custom services and configurations

            // Add session services
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true; // A more secure setting for XSS protection
                options.Cookie.IsEssential = true; // Make the session cookie essential
                options.IdleTimeout = TimeSpan.FromMinutes(30); // You can set a different timeout value
                options.Cookie.Name = ".AccountsLoginSession"; // Set a custom name for session cookie
            });
        }
    }
}