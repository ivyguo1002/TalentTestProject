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
    public static class TokenHelper
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

        public static void SetToken(Token token)
        {
            var script = "localStorage.setItem(arguments[0], arguments[1])";

            Driver.ExecuteScript(script, "access_token", token.TokenId);
            Driver.ExecuteScript(script, "username", token.Username);
            Driver.ExecuteScript(script, "expiry_on", token.Expires);
            Driver.ExecuteScript(script, "talent-permission-scope", $"[\"{token.UserRole}\"]");   
        }
    }
}
