
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils;



namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{

    public class AccountAuthenticationBackend
    {
        private readonly IAccountService _accountService;

        public AccountAuthenticationBackend(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> Authenticate(string username, string password)
        {
            var accounts = await _accountService.FindByUsername(username);
            Account account = accounts.FirstOrDefault<Account>();
            if (account != null)
            {
                if (BcryptPasswordVerifier.AuthenticateUser(password, account.password))
                {
                    return account;
                }
            }
            return null;
        }

        public async Task<Account> GetUser(string userId)
        {
            try
            {
                return await _accountService.GetAccount(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
