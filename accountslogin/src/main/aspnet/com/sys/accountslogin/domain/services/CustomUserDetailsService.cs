using System;
using Microsoft.Extensions.Logging;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.exceptions;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.userdetails;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services
{

    public class CustomUserDetailsService : UserDetailsService
    {
        private readonly ILogger<CustomUserDetailsService> _logger;
        private readonly IAccountService _accountService;

        public CustomUserDetailsService(ILogger<CustomUserDetailsService> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public override async Task<CustomUserDetails> LoadUserByUsernameAsync(string username)
        {
            var accounts = await _accountService.FindByUsername(username);
            Account account = accounts.FirstOrDefault<Account>();

            if (account != null)
            {
                var customUserDetails = new CustomUserDetails();
                customUserDetails.SetByAccount(account);
                return customUserDetails;
            }
            else
            {
                _logger.LogError($"Account not found for username: {username}");
                throw new UsernameNotFoundException($"Account Not Found for {username}");
            }

        }
    }

}
