using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{
    public class SecurityContextHolder
    {
        private static SecurityContextHolder _instance;
        private static readonly object _lock = new object();
        private static ILogger<SecurityContextHolder> _logger;
        private ConcurrentDictionary<string, SecurityContext> _securityContexts;

        private SecurityContextHolder()
        {
            _securityContexts = new ConcurrentDictionary<string, SecurityContext>();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Configure the logger to output to the console
            });
            _logger = loggerFactory.CreateLogger<SecurityContextHolder>();
        }

        public static SecurityContextHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SecurityContextHolder();
                        }
                    }
                }
                return _instance;
            }
        }

        public SecurityContext GetContext(string key)
        {
            if (!_securityContexts.TryGetValue(key, out SecurityContext context))
            {
                _logger.LogWarning("SecurityContext is not initialized for key: {Key}. Initializing a new context.", key);
                context = new SecurityContext();
                _securityContexts[key] = context;
            }
            return context;
        }

        public void SetContext(string key, SecurityContext context)
        {
            _securityContexts[key] = context;
            _logger.LogInformation("SecurityContext set for key: {Key}.", key);
        }

        public void ClearContext(string key)
        {
            if (_securityContexts.TryRemove(key, out SecurityContext context))
            {
                _logger.LogInformation("SecurityContext cleared for key: {Key}.", key);
            }
            else
            {
                _logger.LogWarning("Failed to clear SecurityContext for key: {Key} or it was already null.", key);
            }
        }
    }
}
