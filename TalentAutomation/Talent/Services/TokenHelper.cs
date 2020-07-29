using Framework.Selenium;
using Framework.Utils;
using GTIO.Framework.Services.IdentityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Talent.Enums;
using Talent.Models;

namespace Talent.Services
{
    public class TokenHelper
    {
        public static Token TalentToken => GetTokenAs(Role.talent);
        public static Token EmployerToken => GetTokenAs(Role.employer);
        public static Token RecruiterToken => GetTokenAs(Role.recruiter);

        private static Token GetTokenAs(Role role)
        {
            var users = JsonDataHelper.ToObject<List<User>>("Config\\credentials.json");
            var testUser = users.Where(user => user.Role == role.ToString()).SingleOrDefault();
            var tokenObject = IdentityAPI.GetToken(testUser.Email, testUser.Password);
            return tokenObject;
        }

        public static void SetToken(Token token, Driver driver)
        {
            var script = "localStorage.setItem(arguments[0], arguments[1])";

            driver.ExecuteScript(script, "access_token", token.TokenId);
            driver.ExecuteScript(script, "username", token.Username);
            driver.ExecuteScript(script, "expiry_on", token.Expires);
            driver.ExecuteScript(script, "talent-permission-scope", $"[\"{token.UserRole}\"]");   
        }
    }
}
