using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Talent.Models;
using Talent.Pages;
using TechTalk.SpecFlow;

namespace Talent.Specflow.Steps
{
    [Binding]
    public class AccountSteps
    {
        private PageFactory _pages;
        private List<User> TestUser => JsonDataHelper.ToObject<List<User>>("TestData\\users.json");
        public AccountSteps(PageFactory pages)
        {
            _pages = pages;
        }

        [Given(@"a talent user has set an account")]
        public void GivenATalentUserHasSetAnAccount()
        {

        }

        [Given(@"the user is on Login page")]
        public void GivenTheUserIsOnPage()
        {
            _pages.Login.Open();
        }

        [When(@"the user log in with the valid credential")]
        public void WhenTheUserLogInWithTheValidCredential()
        {
            var user = TestUser.Where(user => user.Key == "valid").SingleOrDefault();
            _pages.Login.LoginAs(user);
        }

        [When(@"the user log in with invalid credential")]
        public void WhenTheUserLogInWithInvalidCredential()
        {
            var user = TestUser.Where(user => user.Key == "invalid").SingleOrDefault();
            _pages.Login.LoginAs(user);
        }

        [Then(@"dashboard page should be shown")]
        public void ThenDashboardPageShouldBeShown()
        {
            Assert.That(_pages.Dashboard.IsLoaded(), Is.True, "Valid User should been logged into system and navigated to Dashboard Page");
        }

        [Then(@"dashboard page should not be shown")]
        public void ThenDashboardPageShouldNotBeShown()
        {
            Assert.That(_pages.Dashboard.IsLoaded(), Is.False, "Invalid User should receive the error message");
        }
    }
}
