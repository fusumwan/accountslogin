using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data;
using accountslogin.Data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;
using System.Text.Json.Nodes;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    [ApiController]
    [Route("usersession")]
    public class UserSessionControllerImpl : UserSessionController
    {
        private readonly IAccountService _accountService;

        private readonly ILogger<UserSessionControllerImpl> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationProperties _applicationProperties;
        private readonly SessionManager _sessionManager;
        private readonly JwtUtil _jwtUtil;
        private readonly WebResponseUtil _webResponseUtil;

        public UserSessionControllerImpl(
            ILogger<UserSessionControllerImpl> logger,
            ApplicationProperties applicationProperties,
            SessionManager sessionManager,
            JwtUtil jwtUtil,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _sessionManager = sessionManager;
            _jwtUtil = jwtUtil;
            _applicationProperties = applicationProperties;
            _webResponseUtil = new WebResponseUtil(_jwtUtil, applicationProperties);
            _accountService = new AccountServiceImpl(context);
        }

        [HttpGet("get_usersession")]
        public async override Task<IActionResult> GetUsersession()
        {
            WebRequest request = new WebRequest(Request);
            _sessionManager.SetRequest(request);
            try
            {
                Account account = null;
                
                var session = _sessionManager.GetSession();
                SecurityContext sc = _sessionManager.GetSecurityContext();
                if (sc != null)
                {
                    Authentication authentication = sc.GetAuthentication();
                    var accounts = await _accountService.FindByUsername(authentication.Principal);
                    account = accounts.FirstOrDefault<Account>();
                }

                if (account == null)
                {
                    account=new Account();
                    account.setAccountid("");
                }
                var json_data=account.ToJson();

                return _webResponseUtil.Response(request, "data", json_data);
            }
            catch (System.Exception ex)
            {
                return ResponseEntity.Response("", statusCode: 500);
            }
        }

        [HttpPost("set_usersession")]
        public async override Task<IActionResult> SetUsersession([FromBody] Account account)
        {
            WebRequest request = new WebRequest(Request);
            _sessionManager.SetRequest(request);
            try
            {
                if (account != null)
                {
                    Authentication auth_obj=new Authentication();
                    CustomUserDetails customUserDetails = new CustomUserDetails();
                    customUserDetails.SetByAccount(account);
                    auth_obj.Principal = customUserDetails.GetUsername();
                    auth_obj.Credentials = null;
                    auth_obj.Authorities = (ICollection<SimpleGrantedAuthority>)customUserDetails.GetAuthorities();
                    auth_obj.Details = customUserDetails;
                    auth_obj.Authenticated = true;

                    // Log successful login
                    SecurityContext sc = SecurityContextHolder.Instance.GetContext(request.Session.Id);
                    // Store the Authentication object in the SecurityContext
                    sc.SetAuthentication(auth_obj);
                    // Bind the SecurityContext to the current thread
                    SecurityContextHolder.Instance.SetContext(request.Session.Id, sc);
                    _sessionManager.SaveSecurityContext( sc);
                    _sessionManager.SetAttribute("Account", account.ToJsonString());
                    _sessionManager.SetAttribute("PageSession", (new PageSession()).ToJsonString());
                    var json_data = JsonUtil.ToJson(account);
                    return _webResponseUtil.Response(request, "data", json_data);
                }
                else
                {
                    return ResponseEntity.Response("", statusCode: 500);
                }
            }
            catch (System.Exception ex)
            {
                return ResponseEntity.Response("", statusCode: 500);
            }
        }

        [HttpGet("get_pagesession")]
        public async override Task<IActionResult> GetPagesession()
        {
            WebRequest request = new WebRequest(Request);
            _sessionManager.SetRequest(request);
            try
            {
                var session = _sessionManager.GetSession();
                var pageSession = session.GetAttribute("PageSession");
                if (string.IsNullOrEmpty(pageSession))
                    return NotFound("No page session found.");

                var json_data = JsonUtil.ToJson(pageSession);
                return _webResponseUtil.Response(request, "data", json_data);
            }
            catch (System.Exception ex)
            {
                return ResponseEntity.Response("", statusCode: 500);
            }
        }

        [HttpPost("set_pagesession")]
        public async override Task<IActionResult> SetPagesession([FromBody] PageSession pageSession)
        {
            WebRequest request = new WebRequest(Request);
            _sessionManager.SetRequest(request);
            try
            {
                if (!_sessionManager.GetSession().IsContainAttribute("Account")){
                    _sessionManager.SetAttribute("Account", (new Account()).ToJsonString());
                }
                _sessionManager.SetAttribute("PageSession", pageSession.ToJsonString());
                var json_data = JsonUtil.ToJson(pageSession);
                return _webResponseUtil.Response(request, "data", json_data);
            }
            catch (System.Exception ex)
            {
                return ResponseEntity.Response("", statusCode: 500);
            }
        }


        [HttpGet("get_profile")]
        public async override Task<IActionResult> GetProfile()
        {
            WebRequest request = new WebRequest(Request);
            _sessionManager.SetRequest(request);
            try
            {
                // Log successful login
                SecurityContext securityContext = SecurityContextHolder.Instance.GetContext(request.Session.Id);
                if (securityContext != null)
                {
                    // Get user authentication information from the security context
                    Authentication authentication = securityContext.GetAuthentication();
                    if (authentication != null && authentication.Authenticated)
                    {
                        // Get the user's name (usually a username or other identifying information)
                        string username = (string)authentication.Principal;
                        var accounts = await _accountService.FindByUsername(username);
                        Account account = accounts.FirstOrDefault();
                        //Here we can use the username to perform related operations
                        // For example, load the user's profile and display it on the user profile page
                        if (account != null)
                        {
                            // Convert session to JSON using JsonUtil.ToJson(session)
                            JsonObject json_data = JsonUtil.ToJson(account); // Implement this method
                            return this._webResponseUtil.Response(request, "data", json_data);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return ResponseEntity.Response("", statusCode: 500);
            }
            return ResponseEntity.Response("", statusCode: 500);
        }
    }
}
