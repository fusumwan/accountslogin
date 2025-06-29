using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public string? account_id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("first_name")]
        public string? first_name { get; set; }

        [Required]
        [StringLength(255)]
        [Column("last_name")]
        public string? last_name { get; set; }

        [Required]
        [StringLength(255)]
        [Column("username")]
        public string? username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Column("email")]
        public string? email { get; set; }

        [StringLength(255)]
        [Column("contact_number")]
        public string? contact_number { get; set; }

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string? password { get; set; }

        [Required]
        [StringLength(255)]
        [Column("user_type")]
        public string? user_type { get; set; }


        public string getAccountid()
        {
            return account_id;
        }

        public void setAccountid(string value)
        {
            account_id = value;
        }

        public string getFirstname()
        {
            return first_name;
        }

        public void setFirstname(string value)
        {
            first_name = value;
        }

        public string getLastname()
        {
            return last_name;
        }

        public void setLastname(string value)
        {
            last_name = value;
        }

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string value)
        {
            username = value;
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string value)
        {
            email = value;
        }

        public string getContactnumber()
        {
            return contact_number;
        }

        public void setContactnumber(string value)
        {
            contact_number = value;
        }

        public string getPassword()
        {
            return password;
        }

        public void setPassword(string value)
        {
            password = value;
        }

        public string getUsertype()
        {
            return user_type;
        }

        public void setUsertype(string value)
        {
            user_type = value;
        }

        public JsonObject ToJson()
        {
            string json_string = this.ToJsonString();
            try
            {
                return (JsonObject)JsonNode.Parse(json_string);
            }
            catch (Newtonsoft.Json.JsonException ex)
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

        public static Account FromJsonString(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<Account>(jsonStr);
            }
            catch (JsonReaderException ex)
            {
                // Handle or log the parsing error
                throw new InvalidOperationException("JSON parsing error: " + ex.Message);
            }
        }
    }
}
