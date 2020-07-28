using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Talent.Models;
using Talent.Pages.Login;
using Talent.Tests.Base;
using static Talent.Pages.PageFactory;

namespace Talent.Tests.Tests.Account
{

    [TestFixture, Category("Account"), Category("Regression")]
    public class LoginTest : BaseTest
    {
        static List<User> validUser = JsonDataHelper.ToObject<List<User>>("TestData\\users.json").Where(x => x.Key == "valid").ToList();

        [Test, Category("Valid"), Category("Login")]
        [Description("Validate that user logs into system with valid credential successfully")]
        [TestCaseSource("validUser")]
        public void LoginWithValidCredential(User user)
        {
            Login.LoginAs(user);
            Assert.That(Dashboard.IsLoaded(), Is.True, "Valid User should been logged into system and navigated to Dashboard Page");
        }

        static List<User> invalidUser = JsonDataHelper.ToObject<List<User>>("TestData\\users.json").Where(x => x.Key == "invalid").ToList();

        [Test, Category("Invalid"), Category("Login")]
        [Description("Validate that user fails to log into system with invalid credential")]
        [TestCaseSource("invalidUser")]
        public void LoginWithInvalidCredential(User user)
        {
            Login.LoginAs(user);
            Assert.That(Dashboard.IsLoaded(), Is.False, "Invalid User should receive the error message");
        }

        static List<User> validUsers = ExcelDataHelper.ReadExcel<User>("TestData\\users.xlsx", "credentials").Where(x => x.Key == "valid").ToList();

        [Test]
        [TestCaseSource("validUsers")]
        [Description("Check Excel Data Reader")]
        public void LoginAs(User user)
        {
            Login.LoginAs(user);
            Assert.That(Dashboard.IsLoaded(), Is.True, "Valid User should been logged into system and navigated to Dashboard Page");
        }
    }

}
