using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Claims;
using System.Text;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http
{
    public class WebRequest
    {
        private readonly HttpRequest _request;
        private object _stream_body { get; set; }

        public WebRequest(HttpRequest request)
        {
            _request = request;
            this.ReadStream(request);
        }
        private async void ReadStream(HttpRequest request)
        {
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                this._stream_body = await reader.ReadToEndAsync();
            };
        }

        public object Body
        {
            get
            {
                return this._stream_body;
            }
        }

        public string BuildAbsoluteUri(string location = null)
        {
            return _request.GetDisplayUrl();
        }

        public ISession Session
        {
            get => _request.HttpContext.Session;
            set => _request.HttpContext.Session = value; // This setter might not be directly possible depending on session handling.
        }

        public ClaimsPrincipal User => _request.HttpContext.User;

        public HttpRequest HttpRequest => _request;


        public IQueryCollection Query => _request.Query;

        public IFormCollection Form => _request.HasFormContentType ? _request.Form : null;

        public IRequestCookieCollection Cookies => _request.Cookies;

        public IHeaderDictionary Headers => _request.Headers;

        public IFormFileCollection Files => _request.HasFormContentType ? _request.Form.Files : null;

        public string Path => _request.Path;

        public string PathInfo => _request.Path;

        public string Method => _request.Method;

        // ASP.NET Core does not use resolver_match; consider middleware or routing data
        // public object ResolverMatch => Not directly translatable to ASP.NET Core

        public string ContentType => _request.ContentType;

        // ASP.NET Core does not have content_params equivalent
        // public object ContentParams => Not directly translatable to ASP.NET Core

        public void Clear()
        {
            // Clear or reset the request object. ASP.NET Core request objects are not typically "cleared" as they are tied directly to the lifecycle of a request.
        }
    }
}
