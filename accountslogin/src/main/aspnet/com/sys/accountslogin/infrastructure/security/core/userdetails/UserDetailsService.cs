namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.userdetails
{

    public abstract class UserDetailsService
    {
        public abstract object LoadUserByUsernameAsync(string username);
    }
}
