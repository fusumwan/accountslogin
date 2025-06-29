using Newtonsoft.Json;
using System;
using System.Text.Json.Nodes;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data
{
    public class Index
    {
        public string SearchCriteria1 { get; set; }
        public string SearchCriteria2 { get; set; }
        public string SearchCriteria3 { get; set; }
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

        public static Index FromJsonString(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<Index>(jsonStr);
            }
            catch (JsonReaderException ex)
            {
                // Handle or log the parsing error
                throw new InvalidOperationException("JSON parsing error: " + ex.Message);
            }
        }
    }

    public class Result
    {
        public string SearchCriteria1 { get; set; }
        public string SearchCriteria2 { get; set; }
        public string SearchCriteria3 { get; set; }

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

        public static Result FromJsonString(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<Result>(jsonStr);
            }
            catch (JsonReaderException ex)
            {
                // Handle or log the parsing error
                throw new InvalidOperationException("JSON parsing error: " + ex.Message);
            }
        }
    }

    public class PageSession
    {
        public Index Index { get; set; }
        public string Login { get; set; }
        public string Signup { get; set; }
        public string AccountInfo { get; set; }
        public Result Result { get; set; }


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

        public static PageSession FromJsonString(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<PageSession>(jsonStr);
            }
            catch (JsonReaderException ex)
            {
                // Handle or log the parsing error
                throw new InvalidOperationException("JSON parsing error: " + ex.Message);
            }
        }
    }
}
