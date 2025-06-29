

using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers.accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.logging;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core;
using accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.security.core.authority;
using System.Text.Json.Nodes;

namespace accountslogin.sandbox
{
    public class JsonConvertTest
    {
        public void test()
        {
            ILogger<JsonConvertTest> logger = LogManager.GetLogger<JsonConvertTest>();
            SimpleGrantedAuthority simpleGrantedAuthority=new SimpleGrantedAuthority("a");
            string simpleGrantedAuthorityJsonString= simpleGrantedAuthority.ToJsonString();
            simpleGrantedAuthority = SimpleGrantedAuthority.FromJsonString(simpleGrantedAuthorityJsonString);

            Account account = new Account();
            account.username = "admin";
            account.password = "password";
            account.user_type = "user";

            string accountJsonString = account.ToJsonString();
            account = Account.FromJsonString(accountJsonString);

            CustomUserDetails customUserDetails = new CustomUserDetails();
            customUserDetails.SetByAccount(account);
            string customUserDetailsJsonString = customUserDetails.ToJsonString();
            customUserDetails= CustomUserDetails.FromJsonString(customUserDetailsJsonString);

            Authentication authentication = new Authentication();
            authentication.Authorities = customUserDetails.Authorities;
            authentication.Details = customUserDetails;
            authentication.Credentials = "password";
            authentication.Principal = "admin";
            authentication.Authenticated = true;


            string authenticationJsonObjectJsonString= authentication.ToJsonString();
            Console.WriteLine(authenticationJsonObjectJsonString);

            authentication=Authentication.FromJsonStr(authenticationJsonObjectJsonString);

            SecurityContext sc=new SecurityContext();
            sc.SetAuthentication(authentication);
            string sc_json_str = sc.ToJsonString();
            sc = SecurityContext.FromJsonStr(sc_json_str);
            logger.LogInformation(sc_json_str);

        }
    }
}
