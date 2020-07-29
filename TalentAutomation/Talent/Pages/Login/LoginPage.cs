using Framework.Selenium;
using Framework.Utils;
using OpenQA.Selenium;
using Talent.Models;
using Talent.Pages.Base;
using Talent.Pages.Dashboard;

namespace Talent.Pages.Login
{
    public partial class LoginPage : BasePage
    {
        public override string Title { get; set; } = "Login";
        private Element EmailTextBox => Driver.FindElement(By.Id("email"));
        private Element PasswordTextBox => Driver.FindElement(By.Id("password"));
        private Element LoginBtn => Driver.FindElement(By.Id("btn_login"));

        public LoginPage(Driver driver) : base(driver) {}

        public void LoginAs(User user)
        {
            //ReportHelper.LogTestStepInfo($"Log in as user Email: {user.Email} Password:{user.Password}");
            EmailTextBox.SendKeys(user.Email);
            PasswordTextBox.SendKeys(user.Password);
            LoginBtn.Click();
        }


    }
}