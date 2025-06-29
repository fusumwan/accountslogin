using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{
    public static class JsonUtil
    {
        public static JsonObject ToJson(object arg0)
        {
            // Check if the object has a method named 'ToJson'
            MethodInfo toJsonMethod = arg0.GetType().GetMethod("ToJson", BindingFlags.Public | BindingFlags.Instance);

            if (toJsonMethod != null && toJsonMethod.ReturnType == typeof(JsonObject) && toJsonMethod.GetParameters().Length == 0)
            {
                // If such a method exists, call it dynamically and return its result
                return (JsonObject)toJsonMethod.Invoke(arg0, null);
            }
            else if (arg0 is IEnumerable<object> list) // Check if arg0 is a list
            {
                var jsonArray = new JsonArray();
                foreach (var item in list)
                {
                    jsonArray.Add(ToJson(item));
                }
                return new JsonObject { ["List"] = jsonArray };
            }
            else if (arg0 is IdentityUser model) // Assuming IdentityUser is a class for user models
            {
                return ToModelJson(model);
            }
            else
            {
                // Serialize other types of objects
                var jsonStr = JsonSerializer.Serialize(arg0, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new CustomConverter() } // Assuming CustomConverter is defined elsewhere
                });
                return JsonNode.Parse(jsonStr).AsObject();
            }
        }

        public static JsonObject ToModelJson(IdentityUser model)
        {
            // Direct serialization for model instances, assuming they're properly annotated for System.Text.Json
            // or have custom converters if needed
            var jsonStr = JsonSerializer.Serialize(model, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new CustomConverter() }
            });
            return JsonNode.Parse(jsonStr).AsObject();
        }

        public static JsonObject ToJSONObject(string jsonStr)
        {
            try
            {
                var jsonObject = JsonNode.Parse(jsonStr).AsObject();
                return jsonObject;
            }
            catch (JsonException e)
            {
                Console.WriteLine("JSON parsing error: " + e.Message);
                return new JsonObject { ["error"] = "Invalid JSON" };
            }
        }

        private class CustomConverter : JsonConverter<object>
        {
            public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
            {
                // Implement custom serialization logic if needed
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}
