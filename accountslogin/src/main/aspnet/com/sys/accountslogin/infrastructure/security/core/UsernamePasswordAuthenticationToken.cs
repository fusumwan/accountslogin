using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;
using System;
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{


    namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
    {
        public class UsernamePasswordAuthenticationToken : AbstractAuthenticationToken
        {
            private Authentication authentication;

            public UsernamePasswordAuthenticationToken(string username = null, string password = null) : base(username, password)
            {
                this.authentication = new Authentication
                {
                    Principal = username,
                    Credentials = password,
                    Authenticated = false
                };
            }

            public override string GetPrincipal()
            {
                return (string)this.authentication.Principal;
            }

            public override string GetCredentials()
            {
                return (string)this.authentication.Credentials;
            }

            public override bool IsAuthenticated()
            {
                return this.authentication.Authenticated;
            }

            public override void SetAuthenticated(bool isAuthenticated)
            {
                this.authentication.Authenticated = isAuthenticated;
            }

            public Authentication GetAuthentication()
            {
                return this.authentication;
            }
        }
    }

}
