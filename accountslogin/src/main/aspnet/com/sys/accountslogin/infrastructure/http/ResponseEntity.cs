using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Text.Json;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http
{

    public class ResponseEntity 
    {
        public ResponseEntity() { }
        public static JsonResult Response(object content, int statusCode = 200)
        {
            return new JsonResult(content)
            {
                StatusCode = statusCode // Optional: Set the HTTP status code as needed
            };
        }
    }
}
