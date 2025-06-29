
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{

    public class DataClassJsonEncoder : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Check if the type is a data class by seeing if it has the DataContract attribute
            return Attribute.IsDefined(objectType, typeof(System.Runtime.Serialization.DataContractAttribute));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The method will never be called.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Convert data class to dictionary and serialize
            var contractResolver = serializer.ContractResolver as DefaultContractResolver;
            var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dict = new Newtonsoft.Json.Linq.JObject();
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    object propVal = property.GetValue(value, null);
                    if (propVal != null)
                    {
                        dict.Add(property.Name, JToken.FromObject(propVal, serializer));
                    }
                }
            }

            dict.WriteTo(writer);
        }

        public override bool CanRead => false;
    }

}
