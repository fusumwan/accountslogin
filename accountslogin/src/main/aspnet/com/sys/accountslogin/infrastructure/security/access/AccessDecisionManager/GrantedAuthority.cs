using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.io;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.access.AccessDecisionManager
{
    public abstract class GrantedAuthority : Serializable
    {
        public abstract string GetAuthority();
    }
}
