using System;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{

    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message = null, Exception cause = null, bool enableSuppression = true, bool writableStackTrace = true)
            : base(message, cause)
        {
            // The .NET Exception class does not directly support suppression or stack trace flags
            // Handling of these properties would require additional custom implementation
        }
    }

}
