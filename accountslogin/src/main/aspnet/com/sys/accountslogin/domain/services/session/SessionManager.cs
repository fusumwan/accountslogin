using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.logging;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using System.Text.Json;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session
{

    public class SessionManager
    {
        private HttpSession _session;
        private WebRequest _request;
        private readonly ILogger<SessionManager> _logger;

        public SessionManager()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Configure the logger to output to the console
            });
            _logger = loggerFactory.CreateLogger<SessionManager>();
            _logger.LogInformation("A new instance of SessionManager was created.");
        }

        public SessionManager SetRequest(WebRequest request)
        {
            _request = request;
            _session = null;
            if (_session == null && _request!=null)
            {
                _session = new HttpSession(_request.HttpRequest);
            }
            return this;
        }

        public HttpSession CreateSession()
        {
            if (_request == null)
            {
                _logger.LogError("Failed to create session: Request object is None.");
                throw new InvalidOperationException("Request object cannot be None.");
            }
            _session = new HttpSession(_request.HttpRequest);
            _logger.LogInformation("Session created successfully.");
            return this.GetSession();
        }

        public HttpSession GetSession()
        {
            if (_session == null)
            {
                _logger.LogInformation("Session not found, creating new session.");
                _session =  CreateSession();
            }
            return _session;
        }

        public string GetAttribute(string attributeName)
        {
            var session = GetSession();
            return session.GetAttribute(attributeName);
        }

        public void SetAttribute(string attributeName, string value)
        {
            
            var session = GetSession();
            session.SetAttribute(attributeName, value);
            _logger.LogInformation($"Set attribute '{attributeName}' in session.");
        }

        public void RemoveAttribute(string attributeName)
        {
            var session =  GetSession();
            session.RemoveAttribute(attributeName);
            _logger.LogInformation($"Removed attribute '{attributeName}' from the session.");
        }


        public void SaveSecurityContext(SecurityContext sc)
        {
            if (sc != null)
            {
				_session.Session.SetString("SecurityContext", sc.ToJsonString());
				_logger.LogInformation($"Security context saved for attribute name: SecurityContext");
			}
            
        }

        public SecurityContext GetSecurityContext()
        {
            if (_session == null)
            {
                return null;
            }
            if (!_session.IsContainAttribute("SecurityContext"))
                return null;

            string securityContextJsonstr = _session.Session.GetString("SecurityContext");
            if (string.IsNullOrEmpty(securityContextJsonstr))
            {
                _logger.LogWarning($"No security context found for attribute name: SecurityContext");
                return null;
            }

            try
            {
                SecurityContext sc=SecurityContext.FromJsonStr(securityContextJsonstr);
                return sc;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Error deserializing SecurityContext: {ex.Message}");
                return null;
            }
        }


        public string GetSessionUsername()
        {
            var session = GetSession();
            var accountJson = session.GetAttribute("Account")?.ToString();
            if (!string.IsNullOrEmpty(accountJson))
            {
                var account = JsonSerializer.Deserialize<Account>(accountJson);
                return account?.username;
            }
            else
            {
                return null;
            }
        }
    }

}