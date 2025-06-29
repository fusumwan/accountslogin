using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web
{
    public class FormDataParser
    {
        private Dictionary<string, string> fields;

        public FormDataParser(string bodyStr)
        {
            fields = ParseFormData(bodyStr);
        }

        private Dictionary<string, string> ParseFormData(string body)
        {
            // Regex to parse the multipart/form-data format
            var pattern = new Regex(
                @"Content-Disposition: form-data; name=""([^""]+)""\r\n\r\n(.*?)\r\n",
                RegexOptions.Singleline
            );

            var matches = pattern.Matches(body);
            var formFields = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                string name = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();
                formFields[name] = value;
            }

            return formFields;
        }

        public string GetField(string fieldName)
        {
            // Retrieve the value of a form field by name
            return fields.TryGetValue(fieldName, out string value) ? value : string.Empty;
        }
    }
}
