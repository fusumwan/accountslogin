using System;
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{


    public class AbstractAuthenticationToken : Authentication, CredentialsContainer
    {
        private object _principal;
        private object _credentials;
        private bool _authenticated;

        public AbstractAuthenticationToken(object principal = null, object credentials = null, bool authenticated = false)
        {
            _principal = principal;
            _credentials = credentials;
            _authenticated = authenticated;
        }

        public virtual object GetPrincipal()
        {
            return _principal;
        }

        public virtual object GetCredentials()
        {
            return _credentials;
        }

        public virtual bool IsAuthenticated()
        {
            return _authenticated;
        }

        public virtual void SetAuthenticated(bool authenticated)
        {
            if (!typeof(bool).IsAssignableFrom(authenticated.GetType()))
            {
                throw new ArgumentException("authenticated must be a boolean value");
            }
            _authenticated = authenticated;
        }

        public virtual void EraseCredentials()
        {
            _credentials = null;
        }
    }

}
