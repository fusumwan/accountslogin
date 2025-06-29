using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using accountslogin.Data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;
using Microsoft.Extensions.Logging;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils;
using System.Security.Policy;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{

    namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
    {
        [ApiController]
        [Route("account")]
        public class AccountControllerImpl : AccountController
        {
            private readonly IAccountService _accountService;
            private readonly IWebHostEnvironment _environment;
            private readonly ILogger<AccountControllerImpl> _logger;
            private readonly ApplicationDbContext _context;
            private readonly ApplicationProperties _applicationProperties;
            private readonly SessionManager _sessionManager;
            private readonly WebResponseUtil _webResponseUtil;
            private readonly JwtUtil _jwtUtil;

            public AccountControllerImpl(
                IAccountService accountService, 
                IWebHostEnvironment environment,
                ILogger<AccountControllerImpl> logger,
                ApplicationDbContext context,
                ApplicationProperties applicationProperties,
                SessionManager sessionManager,
                JwtUtil jwtUtil
                )
            {
                _accountService = accountService;
                _environment = environment;

                _logger = logger;
                _context = context;
                _sessionManager = sessionManager;
                _jwtUtil = jwtUtil;
                _applicationProperties = applicationProperties;
                _webResponseUtil = new WebResponseUtil(_jwtUtil, applicationProperties);
                
            }

            [HttpPost("create")]
            public override async Task<IActionResult> Create(IFormCollection formData)
            {
                var account = new Account
                {
                    first_name = formData["first_name"],
                    last_name = formData["last_name"],
                    username = formData["username"],
                    email = formData["email"],
                    contact_number = formData["contact_number"],
                    password = formData["password"], // Assume hashing is handled elsewhere
                    user_type = formData["user_type"]
                };
                await _accountService.SaveAccount(account);
                return Ok(JsonConvert.SerializeObject(account));
            }

            [HttpGet("get/{accountId}")]
            public override async Task<IActionResult> Get(string accountId)
            {
                var account = await _accountService.GetAccount(accountId);
                return account != null ? Ok(JsonConvert.SerializeObject(account)) : NotFound("Account not found.");
            }

            [HttpGet("retrieve")]
            public override async Task<IActionResult> Retrieve()
            {
                var accounts = await _accountService.GetAccounts();
                return Ok(JsonConvert.SerializeObject(accounts));
            }

            [HttpPost("update/{accountId}")]
            public override async Task<IActionResult> Update(string accountId, IFormCollection formData)
            {
                var account = await _accountService.GetAccount(accountId);
                if (account == null) return NotFound("Account not found.");

                account.first_name = formData["first_name"];
                account.last_name = formData["last_name"];
                account.email = formData["email"];
                account.contact_number = formData["contact_number"];
                account.password = formData["password"]; // Assume hashing is handled elsewhere
                account.user_type = formData["user_type"];

                await _accountService.SaveAccount(account);
                return Ok(JsonConvert.SerializeObject(account));
            }

            [HttpDelete("delete/{accountId}")]
            public override async Task<IActionResult> Delete(string accountId)
            {
                var account = await _accountService.GetAccount(accountId);
                if (account == null) return NotFound("Account not found.");

                await _accountService.DeleteAccount(accountId);
                return Ok($"Account {accountId} deleted successfully.");
            }

            [HttpPost("filter")]
            public override async Task<IActionResult> Filter(IFormCollection formData)
            {
                // Example filter by username
                var username = formData["username"];
                var accounts = await _accountService.FindByUsername(username);
                return Ok(JsonConvert.SerializeObject(accounts));
            }
            [HttpPost("getByAccountUsernamePassword")]
            public override async Task<IActionResult> GetByAccountUsernamePassword(
                [FromForm] string username_00, 
                [FromForm] string password_01,
                [FromForm] string Limit)
            {
                WebRequest request = new WebRequest(Request);

                var accounts = await _accountService.FindByUsername(username_00);
                Account account = accounts.FirstOrDefault<Account>();
                if (account != null)
                {
                    //Now account is a single Account instance, this will work
                    if (!BcryptPasswordVerifier.AuthenticateUser(password_01, account.getPassword())){
                        account = null;
                    }
                }

                if (account!=null){
                    var json_data = account.ToJson();
                    return this._webResponseUtil.Response(request, "data", json_data);
                }
                else
                {
                    return ResponseEntity.Response("", statusCode: 500);
                }
            }

            [HttpGet("getByAccountUsername/{username}")]
            public override async Task<IActionResult> GetByAccountUsername(string username)
            {
                var accounts = await _accountService.FindByUsername(username);
                Account account = accounts.FirstOrDefault<Account>();
                return Ok(JsonConvert.SerializeObject(accounts));
            }

            [HttpGet("getByAccount/{accountId}")]
            public override async Task<IActionResult> GetByAccount(string accountId)
            {
                var account = await _accountService.GetAccount(accountId);
                return account != null ? Ok(JsonConvert.SerializeObject(account)) : NotFound("Account not found.");
            }

            
        }
    }

}
