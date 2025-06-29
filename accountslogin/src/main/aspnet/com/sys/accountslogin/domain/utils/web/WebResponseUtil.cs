using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Text.Json.Nodes;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.http;
using Azure;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services.session;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web
{
    public class WebResponseUtil
    {
        private readonly JwtUtil _jwtUtil;
        private readonly ApplicationProperties _applicationProperties;
        private readonly SessionManager _sessionManager;
        public WebResponseUtil(JwtUtil jwtUtil, ApplicationProperties applicationProperties)
        {
            _jwtUtil = jwtUtil;
            _sessionManager = new SessionManager();
            _applicationProperties = applicationProperties;
        }

        public IActionResult Response(WebRequest request,string jsonField, object jsonData)
        {
            string url = request.Path;
            if (_applicationProperties.SecurityJwtEnable)
            {
                string sessionKey = request.Session.Id;  // Assuming sessionKey is stored in Session
                _sessionManager.SetRequest(request);
                string username = _sessionManager.GetSessionUsername();
                if (!string.IsNullOrEmpty(username))
                {
                    return ResponseWithJWT(sessionKey, jsonField, jsonData);
                }
                else if (SecurityConfig.IsPermitAllPage(url))
                {
                    JsonObject response = new JsonObject { ["token"] = "" };
                    if (!string.IsNullOrEmpty(jsonField))
                    {
                        response = new JsonObject {
                            ["token"] = "",
                            [jsonField] = (JsonNode)jsonData
                        };
                    }
                    return ResponseEntity.Response(response, statusCode: 200);
                }
                else
                {
                    return ResponseEntity.Response("", statusCode: 401);
                }
            }
            else
            {
                object response = string.IsNullOrEmpty(jsonField) ? jsonData : new { data = jsonData };
                return ResponseEntity.Response(response, statusCode: 200);
            }
        }

        public IActionResult ResponseWithJWT(string sessionKey, string jsonField, object jsonData)
        {
            string username = _sessionManager.GetSessionUsername(); // Retrieve username from session or other means
            if (!string.IsNullOrEmpty(username))
            {
                string token = _jwtUtil.GenerateToken(username);
                var response = new { token = token, data = jsonData };
                return ResponseEntity.Response(response, statusCode: 200);
            }
            else
            {
                return ResponseEntity.Response("", statusCode: 401);
            }
        }
    }
}
