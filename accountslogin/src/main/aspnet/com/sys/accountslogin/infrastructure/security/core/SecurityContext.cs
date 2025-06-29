using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority;
using Mysqlx.Session;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.Json.Nodes;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core
{

    public class SecurityContext
    {
        private Authentication _authentication;

        public SecurityContext(Authentication authentication = null)
        {
            _authentication = authentication;
        }
        public Authentication Authentication
        {
            get => _authentication;
            set => _authentication = value;
        }

        public Authentication GetAuthentication()
        {
            return _authentication;
        }

        public void SetAuthentication(Authentication authentication)
        {
            _authentication = authentication;
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
            return JsonConvert.SerializeObject(new
            {
                Authentication = (this._authentication as Authentication)?.ToJsonString(), // Safely cast and serialize if not null
            }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }


        public static SecurityContext FromJsonStr(string jsonString)
        {
            try
            {
                // Parse the JSON string into a dynamic object to handle nested structures
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

                string authentication_json_str = jsonObject.Authentication.ToString();
                var securityContext = new SecurityContext
                {
                    Authentication = null
                };

                // Deserialize the Authentication object into Authentication
                Authentication authentication = Authentication.FromJsonStr(authentication_json_str);
                securityContext.Authentication = authentication;
                return securityContext;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error parsing the JSON string: " + ex.Message);
            }
        }
    }
}
