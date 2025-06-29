using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.access.AccessDecisionManager;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services
{
    public class CustomUserDetails : UserDetails
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsAccountNonExpired { get; private set; }
        public bool IsAccountNonLocked { get; private set; }
        public bool IsCredentialsNonExpired { get; private set; }
        public bool IsEnabled { get; private set; }
        public List<SimpleGrantedAuthority> Authorities { get; private set; }

        public CustomUserDetails(string username = null, string password = null, List<SimpleGrantedAuthority> authorities = null,
                                 bool isAccountNonExpired = true, bool isAccountNonLocked = true,
                                 bool isCredentialsNonExpired = true, bool isEnabled = true)
        {
            Username = username;
            Password = password;
            IsAccountNonExpired = isAccountNonExpired;
            IsAccountNonLocked = isAccountNonLocked;
            IsCredentialsNonExpired = isCredentialsNonExpired;
            IsEnabled = isEnabled;
            Authorities = authorities ?? new List<SimpleGrantedAuthority>();

        }

        public void SetByAccount(Account account)
        {
            // Existing logic to set properties from Account
            if (account != null)
            {
                Username = account.username;
                Password = account.password;
                IsAccountNonExpired = true;
                IsAccountNonLocked = true;
                IsCredentialsNonExpired = true;
                IsEnabled = true;

                // Ensure that the user_type is not null or whitespace
                if (!string.IsNullOrWhiteSpace(account.user_type))
                {
                    var authority = new SimpleGrantedAuthority(account.user_type);
                    Authorities = new List<SimpleGrantedAuthority> { authority };
                }
                else
                {
                    throw new ArgumentException("User type cannot be null or empty for granted authority.");
                }
            }
        }

        public JsonObject ToJson()
        {
            string json_string = this.ToJsonString();
            try
            {
                return (JsonObject)JsonNode.Parse(json_string);
            }
            catch (JsonException ex)
            {
                // Log the error or handle it according to your error handling policy
                throw new InvalidOperationException("Failed to parse JSON string: " + ex.Message);
            }
        }

        public string ToJsonString()
        {
            // Ensure correct serialization settings
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static CustomUserDetails FromJsonString(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<CustomUserDetails>(jsonStr);
            }
            catch (JsonReaderException ex)
            {
                // Handle or log the parsing error
                throw new InvalidOperationException("JSON parsing error: " + ex.Message);
            }
        }

        public override ICollection<GrantedAuthority> GetAuthorities()
        {
            return this.Authorities.ToArray();
        }

        public override string GetPassword()
        {
            return this.Password;
        }

        public override string GetUsername()
        {
            return this.Username;
        }
    }
}
