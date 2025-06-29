using System.Collections.Generic;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.access.AccessDecisionManager;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{

    public abstract class UserDetails
    {
        public abstract ICollection<GrantedAuthority> GetAuthorities();

        public abstract string GetPassword();

        public abstract string GetUsername();
    }

}
