
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using accountslogin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.viewmodels;
using System.Text.Json;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using System.Text.Json.Nodes;
using Azure;
using System.Security;
using SecurityContext = accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.SecurityContext;
using Azure.Core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.controllers;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    //[Route("Login")]
    public class LoginController : Controller
    {
        private readonly AuthenticationManager _authenticationManager;
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationProperties _applicationProperties;
        private readonly SessionManager _sessionManager;
        private readonly JwtUtil _jwtUtil;
        public LoginController(
            AuthenticationManager accountManager,
            ILogger<LoginController> logger,
            ApplicationProperties applicationProperties,
            SessionManager sessionManager,
            JwtUtil jwtUtil,
            ApplicationDbContext context
            )
        {
            _authenticationManager = accountManager;
            _logger = logger;
            _context = context;
            _sessionManager= sessionManager;
            _jwtUtil = jwtUtil;
            _applicationProperties = applicationProperties;
        }

        [HttpGet]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Default layout logic here
            ViewBag.Layout = "Home";
            return View("LoginContent");
        }



        
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
			WebRequest request = new WebRequest(Request);

            // Bind the SecurityContext to the current thread
            if (request.Session != null)
            {
                SecurityContextHolder.Instance.SetContext(request.Session.Id, null);
                _sessionManager.SetRequest(request);
				_sessionManager.RemoveAttribute("SecurityContext");
				_sessionManager.RemoveAttribute("Account");
				_sessionManager.RemoveAttribute("PageSession");
			}

			await HttpContext.SignOutAsync();
			_logger.LogInformation("User logged out.");

            return RedirectToAction(nameof(IndexController.Index), "Index");
        }

        /// <summary>
        /// This updated LoginController includes a [AllowAnonymous] attribute to allow unauthenticated users 
        /// to access the login page, uses [ValidateAntiForgeryToken] to prevent CSRF attacks, 
        /// and performs a user existence check before attempting to sign in. 
        /// Additionally, a logout method is provided for user sign-out. 
        /// This should address common reasons for ModelState.IsValid being false, 
        /// such as model validation failures or user existence checks.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticateTheUser")]
        [AllowAnonymous]
        [Consumes("application/json")] // Ensures only requests with JSON content type are processed
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthenticateTheUser()
        {
            WebRequest request = new WebRequest(Request);
            LoginViewModel model = new LoginViewModel();

            model.Username = WebRequestUtil.Request(request).SetRequestParameter("username").ToStr();
            model.Password = WebRequestUtil.Request(request).SetRequestParameter("password").ToStr();

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return ResponseEntity.Response(new { success = false, message = "Username or password is missing" }, statusCode: 400);
            }

            // Model Validation: Ensure your LoginViewModel is correctly annotated with validation attributes. It seems your model includes Username and Password fields. Make sure these fields are properly validated for presence and format if necessary.
            // Initial model state check to return early if there are validation issues.
            if (!ModelState.IsValid)
            {
                return ResponseEntity.Response(new { success = false, message = "Invalid login attempt." }, statusCode: 401);
            }

            // Creating an instance of Authentication
            Authentication auth_request = (new UsernamePasswordAuthenticationToken(model.Username, model.Password)).GetAuthentication();
            Authentication auth_result = await _authenticationManager.Authenticate(auth_request);
            if (auth_result.Authenticated)
            {
                // Log successful login
                SecurityContext sc = SecurityContextHolder.Instance.GetContext(request.Session.Id);
                // Store the Authentication object in the SecurityContext
                sc.SetAuthentication(auth_result);
                // Bind the SecurityContext to the current thread
                SecurityContextHolder.Instance.SetContext(request.Session.Id, sc);

                _sessionManager.SetRequest(request);
                _sessionManager.SaveSecurityContext(sc);



                //_logger.LogInformation($"Security context securityContextJsonstr : {securityContextJsonstr}");
                //Console.WriteLine($"Security context securityContextJsonstr : {securityContextJsonstr}");
                var redirectPath = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/" + this._applicationProperties.AppName(false);
                JsonObject data = create_json_object("success", redirectPath, this._applicationProperties, "");
                return ResponseEntity.Response(data, statusCode: 200);

            }
            else
            {
                _logger.LogWarning("Failed login attempt for {username}.", model.Username);
                return ResponseEntity.Response("", 200);
            }

            
        }

        private void SaveSecurityContext(string id, SecurityContext sc)
        {
            HttpContext.Session.SetString(id, sc.ToJsonString());
            _logger.LogInformation($"Security context saved for session: {id}");
        }

        private string GetSecurityContext(string id)
        {
            string securityContextJsonstr = HttpContext.Session.GetString(id);
            if (string.IsNullOrEmpty(securityContextJsonstr))
            {
                _logger.LogWarning($"No security context found for session key: {id}");
                return null;
            }

            try
            {
                return securityContextJsonstr;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Error deserializing SecurityContext: {ex.Message}");
                return null;
            }
        }



        private JsonObject create_json_object(string status, string contextPath, ApplicationProperties applicationProperties, string token)
        {
            JsonObject response = new JsonObject
            {
                ["status"] = status,
                ["redirect"] = contextPath,
                ["token"] = token
            };

            return response;
        }



        private bool is_valid_json_str(string json_string)
        {
            json_string = json_string.Trim();
            try
            {
                JsonDocument.Parse(json_string);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
