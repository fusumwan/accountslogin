using BCrypt.Net;
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{
    public static class BcryptPasswordVerifier
    {
        public static string Encode(string rawPassword)
        {
            // Hash the password with a salt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);
            return hashedPassword;
        }

        public static bool AuthenticateUser(string rawPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(storedPassword))
            {
                // User does not exist
                return false;
            }
            // Compare the input password with the stored hashed password
            return BCrypt.Net.BCrypt.Verify(rawPassword, storedPassword);
        }
    }
}