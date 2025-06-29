using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;
using NuGet.Common;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.logging
{

    public static class LogManager
    {
        public static ILogger<T> GetLogger<T>()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Configure the logger to output to the console
            });
            return loggerFactory.CreateLogger<T>();
        }
    }

}
