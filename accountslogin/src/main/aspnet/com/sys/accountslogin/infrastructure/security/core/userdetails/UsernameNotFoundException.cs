using System;
using System.Security.Authentication;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.userdetails
{

    public class UsernameNotFoundException : AuthenticationException
    {
        public UsernameNotFoundException(string message = null, Exception cause = null, bool enableSuppression = true, bool writableStackTrace = true)
            : base(message, cause, enableSuppression, writableStackTrace)
        {
        }
    }

}
