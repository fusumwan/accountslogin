using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web
{
    public class WebRequestUtil
    {
        private WebRequest request;
        private string requestParameter = string.Empty;
        private string requestParameterBase64 = string.Empty;
        private string pattern = string.Empty;
        private JsonDocument body = null;
        private object stream_body = string.Empty;
        private FormDataParser formData;

        public WebRequestUtil(WebRequest request)
        {
            this.request = request;
            this.stream_body = request.Body;
        }

        public static WebRequestUtil Request(WebRequest request)
        {
            var webRequestUtil = new WebRequestUtil(request);

            try
            {
                if (webRequestUtil.IsFormData())
                {
                    var formParser = new FormDataParser((string)webRequestUtil.GetStreamBody());
                    webRequestUtil.SetFormData(formParser);
                }
                else
                {
                    webRequestUtil.SetBody(JsonDocument.Parse((string)webRequestUtil.GetStreamBody()));
                }
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Invalid JSON");
            }

            return webRequestUtil;
        }

        private void SetBody(JsonDocument body)
        {
            this.body = body;
        }

        private object GetStreamBody()
        {
            return this.stream_body ?? string.Empty;
        }

        private void SetFormData(FormDataParser formData)
        {
            this.formData = formData;
        }

        private bool IsFormData()
        {
            return ((string)this.stream_body).Contains("form-data");
        }

        public string GetRequestParameter()
        {
            return this.requestParameter;
        }

        public WebRequestUtil SetRequestParameter(string requestParameter)
        {
            this.requestParameter = requestParameter;
            return this;
        }

        public string GetRequestParamterBase64()
        {
            return this.requestParameterBase64;
        }

        public void SetRequestParamterBase64(string requestParamterBase64)
        {
            this.requestParameterBase64 = requestParamterBase64;
        }

        public string GetPattern()
        {
            return this.pattern;
        }

        public void SetPattern(string pattern)
        {
            this.pattern = pattern;
        }

        private object GetValue()
        {
            if (IsFormData())
            {
                return this.formData.GetField(this.requestParameter) ?? string.Empty;
            }
            else
            {
                try
                {
                    return this.body?.RootElement.GetProperty(this.requestParameter) ?? new JsonElement();
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
        }

        public int ToByte()
        {
            int.TryParse(GetValue()?.ToString(), out var result);
            return result;
        }

        public int ToShort()
        {
            short.TryParse(GetValue()?.ToString(), out var result);
            return result;
        }

        public int ToInteger()
        {
            int.TryParse(GetValue()?.ToString(), out var result);
            return result;
        }

        public long ToLong()
        {
            long.TryParse(GetValue()?.ToString(), out var result);
            return result;
        }

        public float ToFloat()
        {
            float.TryParse(GetValue()?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
            return result;
        }

        public double ToDouble()
        {
            double.TryParse(GetValue()?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
            return result;
        }

        public decimal ToBigDecimal()
        {
            decimal.TryParse(GetValue()?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
            return result;
        }

        public DateTime ToDate()
        {
            return (DateTime)DateTimeUtil.StringToDate(GetValue()?.ToString(), this.pattern);
        }

        public DateTime ToLocateDate()
        {
            return (DateTime)DateTimeUtil.StringToLocalDate(GetValue()?.ToString(), this.pattern);
        }

        public DateTime ToLocateDateTime()
        {
            return (DateTime)DateTimeUtil.StringToLocalDateTime(GetValue()?.ToString(), this.pattern);
        }

        public string ToStr()
        {
            return GetValue()?.ToString() ?? string.Empty;
        }

        public List<string> ToArray()
        {
            List<string> result = new List<string>();
            if (IsFormData())
            {
                var value = GetValue();
                result.Add(value?.ToString() ?? string.Empty);
            }
            else
            {
                if (GetValue() is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in element.EnumerateArray())
                    {
                        result.Add(item.ToString());
                    }
                }
            }
            return result;
        }

        public int ToBigInteger()
        {
            int.TryParse(GetValue()?.ToString(), out var result);
            return result;
        }
    }
}
