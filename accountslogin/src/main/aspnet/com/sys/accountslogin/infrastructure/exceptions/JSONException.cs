using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.exceptions
{
    public class JSONException : Exception
    {
        public int status { get; set; }
        public string message { get; set; }

        public JSONException(string message, int status = 400) : base(message)
        {
            this.status = status;
            this.message = message;
        }

        public IActionResult ToHttpActionResult()
        {
            var response = new JsonResult(new { status = "error", message = this.message })
            {
                StatusCode = this.status // Setting the HTTP status code
            };
            return response;
        }
    }
}
