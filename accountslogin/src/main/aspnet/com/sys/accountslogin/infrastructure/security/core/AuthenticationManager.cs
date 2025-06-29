using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using accountslogin.Data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils;
using Microsoft.EntityFrameworkCore;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{
    public class AuthenticationManager
    {
        private readonly ILogger<AuthenticationManager> _authenticationManagerLogger;
        private readonly ILogger<CustomUserDetailsService> _customUserDetailsServiceLogger;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public AuthenticationManager(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            ILogger<AuthenticationManager> authenticationManagerLogger,
            ILogger<CustomUserDetailsService> customUserDetailsServiceLogger)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _authenticationManagerLogger = authenticationManagerLogger;
            _customUserDetailsServiceLogger = customUserDetailsServiceLogger;

        }
        

        public async Task<Authentication> Authenticate(Authentication auth_request, bool isPersistent = false)
        {
            string username = (string)auth_request.Principal;
            string password = (string)auth_request.Credentials;
            var authentication = new Authentication();
            try
            {
                IAccountService accountService = new AccountServiceImpl(this._context);
                AccountAuthenticationBackend accountAuthenticationBackend = new AccountAuthenticationBackend(accountService);
                var account = await accountAuthenticationBackend.Authenticate(username, password);
                if (account != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.account_id.ToString()),
                        new Claim(ClaimTypes.Name, account.username),
                        new Claim(ClaimTypes.Role, account.user_type) // Assuming 'user_type' is a string corresponding to a role
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = isPersistent });

                    
                    // Handling user found scenario without using login() due to custom user model compatibility issues.            
                    var customUserDetailsService = new CustomUserDetailsService(this._customUserDetailsServiceLogger, accountService);
                    CustomUserDetails customUserDetails = await customUserDetailsService.LoadUserByUsernameAsync(username);
                    authentication.Principal = customUserDetails.Username;
                    authentication.Credentials = null; // It's a good practice not to store the password.
                    authentication.Authorities = customUserDetails.Authorities;
                    authentication.Details = customUserDetails;

                    
                    authentication.Authenticated = true;
                }
                else
                {
                    authentication.Authenticated = false;

                }
            }
            catch (Exception ex)
            {
                _authenticationManagerLogger.LogError($"Error during authentication: {ex.Message}");
                authentication.Authenticated = false;
            }
            return authentication;
        }
    }
}
