using System;
using System.Security.Principal;
using System.Text.Json.Nodes;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.access.AccessDecisionManager;
using Newtonsoft.Json;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority
{

    public class SimpleGrantedAuthority : GrantedAuthority
    {
        public string Role { get; private set; }

        public SimpleGrantedAuthority(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("A granted authority textual representation is required");
            }
            this.Role = role;
        }


        public override string GetAuthority()
        {
            return Role;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is SimpleGrantedAuthority authority)
                return Role == authority.Role;
            return false;
        }

        public override int GetHashCode()
        {
            return Role.GetHashCode();
        }

        public override string ToString()
        {
            return Role;
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
                Role = this.Role,

            }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static SimpleGrantedAuthority FromJsonString(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<SimpleGrantedAuthority>(jsonStr);
            return new SimpleGrantedAuthority(data.Role);
        }

        public override string Serialize()
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(string data)
        {
            throw new NotImplementedException();
        }
    }

}
