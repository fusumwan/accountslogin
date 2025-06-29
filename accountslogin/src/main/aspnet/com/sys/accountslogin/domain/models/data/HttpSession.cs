using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using System.Text.Json;

public class HttpSession
{
    private readonly HttpRequest _request;
    private readonly ILogger<HttpSession> _logger;

    public HttpSession(HttpRequest request)
    {
        _request = request;

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole(); // Configure the logger to output to the console
        });
        _logger = loggerFactory.CreateLogger<HttpSession>();
    }

    public string SessionKey => _request.HttpContext.Session.Id;

    public ISession Session => _request.HttpContext.Session;

    public void SetAttribute(string attributeName, string attributeValue)
    {
        Session.SetString(attributeName, attributeValue);
        _logger.LogInformation($"Attribute '{attributeName}' set to session.");
    }

    public string GetAttribute(string attributeName)
    {
        string value = Session.GetString(attributeName);
        return value != null ? value : null;
    }

    public bool IsContainAttribute(string attributeName)
    {
        return Session.GetString(attributeName) != null;
    }

    public void RemoveAttribute(string attributeName)
    {
        if (IsContainAttribute(attributeName))
        {
            Session.Remove(attributeName);
            _logger.LogInformation($"Attribute '{attributeName}' removed from session.");
        }
        else
        {
            _logger.LogWarning($"Attempt to clear non-existent attribute '{attributeName}'.");
        }
    }

    public void Clear()
    {
        Session.Clear();
        _logger.LogInformation("All session attributes cleared.");
    }

    public void Save()
    {
        Session.CommitAsync().Wait();
    }
}
