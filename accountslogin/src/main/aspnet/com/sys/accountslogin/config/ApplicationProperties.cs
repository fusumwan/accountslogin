using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http; // For accessing IHttpContextAccessor

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.config
{
    public class ApplicationProperties
    {
        private readonly ILogger<ApplicationProperties> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationProperties(ILogger<ApplicationProperties> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool SecurityJwtEnable
        {
            get
            {
                // Adjusted to match the JSON structure
                var value = _configuration.GetValue<bool>("Customs:security.jwt.enable");
                _logger.LogInformation($"Loading security.jwt.enable: {value}");
                return value;
            }
        }
        public string? GetDateConvertDateFormatPattern()
        {
            return null;
        }
        public string? GetDateConvertTimeFormatPattern()
        {
            return null;
        }
        public DateTime? GetDateConvertDateTimePattern()
        {
            return null;
        }
        public string? GetLocalDateTimeConvertDateFormatPattern()
        {
            return null;
        }
        public string? AppName(bool bQuotes = false)
        {
            // Adjusted to match the JSON structure
            var appName = _configuration.GetValue<string>("Customs:AppName");
            _logger.LogInformation($"Loading the AppName: {appName}");
            return bQuotes ? $"\"{appName}\"" : appName;
        }

        public string ContextPath()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var request = _httpContextAccessor.HttpContext.Request;
                var host = request.Host.ToUriComponent();
                var pathBase = request.PathBase.ToUriComponent(); // Similar to context path in Java

                return $"{request.Scheme}://{host}{pathBase}";
            }
            return "";
        }
    }
}
