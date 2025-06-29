using Newtonsoft.Json;
using System.Collections.Generic;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{
    public class FilterRequestDataSerializer
    {
        public static FilterRequestData? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<FilterRequestData>(json);
        }

        public static string Serialize(FilterRequestData requestData)
        {
            return JsonConvert.SerializeObject(requestData);
        }

        public FilterRequestData Create(Dictionary<string, object> validatedData)
        {
            var requestData = new FilterRequestData();
            if (validatedData.ContainsKey("hql"))
            {
                requestData.SetHql(validatedData["hql"] as string);
            }
            if (validatedData.ContainsKey("dataValues"))
            {
                requestData.SetDataValues(validatedData["dataValues"] as Dictionary<string, List<string>> ?? new Dictionary<string, List<string>>());
            }
            return requestData;
        }

        public void Update(FilterRequestData instance, Dictionary<string, object> validatedData)
        {
            if (validatedData.ContainsKey("hql"))
            {
                instance.SetHql(validatedData["hql"] as string);
            }

            if (validatedData.ContainsKey("dataValues"))
            {
                instance.SetDataValues(validatedData["dataValues"] as Dictionary<string, List<string>>);
            }
        }
    }
}
