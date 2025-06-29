using System;
using System.Collections.Generic;

using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web;



namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{


    public static class FilterFieldTypeConverter
    {
        public static FilterRequestData FilterFieldToParamValue(FilterRequestData requestData, TableFieldCollection tableFieldCollection, ApplicationProperties applicationProperties)
        {
            var dataValues = requestData.GetDataValues();
            foreach (var paramName in new List<string>(dataValues.Keys))
            {
                if (paramName.Contains("_"))
                {
                    string columnName = paramName.Substring(0, paramName.LastIndexOf("_"));
                    string dataType = tableFieldCollection.FindDataType(columnName);
                    var paramValue = dataValues[paramName];

                    if (paramValue.Count > 1)
                    {
                        for (int i = 0; i < paramValue.Count; i++)
                        {
                            string value = paramValue[i]?.Trim();
                            if (!string.IsNullOrEmpty(value))
                            {
                                paramValue[i] = (string)ConvertValue(dataType, value, applicationProperties);
                            }
                        }
                    }
                    else if (paramValue.Count == 1)
                    {
                        string value = paramValue[0]?.Trim();
                        if (!string.IsNullOrEmpty(value))
                        {
                            paramValue[0] = (string)ConvertValue(dataType, value, applicationProperties);
                        }
                    }
                    dataValues[paramName] = paramValue;
                }
            }
            requestData.SetDataValues(dataValues);
            return requestData;
        }

        public static object ConvertValue(string dataType, string value, ApplicationProperties applicationProperties)
        {
            switch (dataType)
            {
                case "date":
                    return DateTimeUtil.StringToDate(value, applicationProperties.GetDateConvertDateFormatPattern());
                case "datetime":
                    return DateTimeUtil.StringToLocalDateTime(value, applicationProperties.GetLocalDateTimeConvertDateFormatPattern());
                case "int":
                case "bigint":
                case "decimal":
                    return int.Parse(value);
                case "double":
                case "float":
                    return double.Parse(value);
                case "boolean":
                    return value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("1");
                default:
                    return value;
            }
        }
    }


}