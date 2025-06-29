using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority;
using Newtonsoft.Json;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{

    public class Authentication
    {
        private ICollection<SimpleGrantedAuthority> _authorities;
        private string _principal;
        private string _credentials;
        private CustomUserDetails _details;
        private bool _authenticated;

        public Authentication(string principal = null, string credentials = null, ICollection<SimpleGrantedAuthority> authorities = null, CustomUserDetails details = null, bool authenticated = false)
        {
            _principal = principal;
            _credentials = credentials;
            _authorities = authorities ?? new List<SimpleGrantedAuthority>();
            _details = details;
            _authenticated = authenticated;
        }

        public string Principal
        {
            get => _principal;
            set => _principal = value;
        }

        public string Credentials
        {
            get => _credentials;
            set => _credentials = value;
        }

        public ICollection<SimpleGrantedAuthority> Authorities
        {
            get => _authorities;
            set => _authorities = value;
        }

        public bool ContainsAuthority(SimpleGrantedAuthority authority)
        {
            return authority != null && _authorities.Any(a => a.GetAuthority() == authority.GetAuthority());
        }

        public CustomUserDetails Details
        {
            get => _details;
            set => _details = value;
        }

        public bool Authenticated
        {
            get => _authenticated;
            set => _authenticated = value;
        }

        public JsonObject ToJson()
        {
            string json_string = this.ToJsonString();
            return (JsonObject)JsonObject.Parse(json_string);
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(new
            {
                Principal = this.Principal,
                Credentials = this.Credentials,
                Details = (this.Details as CustomUserDetails)?.ToJsonString(), // Safely cast and serialize if not null
                Authorities = this.Authorities.Select(a => a.ToJsonString()).ToList(),
                Authenticated = this.Authenticated
            }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static Authentication FromJsonStr(string jsonString)
        {
            try
            {
                // Parse the JSON string into a dynamic object to handle nested structures
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

                var authentication = new Authentication
                {
                    Principal = jsonObject.Principal,
                    Credentials = jsonObject.Credentials,
                    Authenticated = jsonObject.Authenticated,
                    Authorities = new List<SimpleGrantedAuthority>()
                };

                // Deserialize the Details object into CustomUserDetails
                if (jsonObject.Details != null)
                {
                    authentication.Details = JsonConvert.DeserializeObject<CustomUserDetails>(jsonObject.Details.ToString());
                }

                // Handle the Authorities array
                foreach (var authority in jsonObject.Authorities)
                {
                    var grantedAuthority = JsonConvert.DeserializeObject<SimpleGrantedAuthority>(authority.ToString());
                    authentication.Authorities.Add(grantedAuthority);
                }

                return authentication;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error parsing the JSON string: " + ex.Message);
            }
        }

        private class AuthenticationJsonData
        {
            public object principal { get; set; }
            public object credentials { get; set; }
            public List<string> authorities { get; set; }
            public string details { get; set; }
            public bool authenticated { get; set; }
        }
    }

}
