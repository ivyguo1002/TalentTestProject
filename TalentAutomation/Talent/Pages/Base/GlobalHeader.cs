using Framework.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Talent.Pages.Jobs;
using Talent.Pages.Profile;

namespace Talent.Pages.Base
{
    public class GlobalHeader
    {
        public Driver Driver { get; set; }
        public GlobalHeader(Driver driver)
        {
            Driver = driver;
        }
        private Element JobsMenu => Driver.FindElement(By.XPath("//*[@class='ant-menu-submenu-title'][contains(.,'Jobs')]"));

        private Element ExploreJobsMenu => Driver.FindElement(By.CssSelector("a[href='/jobs/explore']"));

        private Element ProfileMenu => Driver.FindElement(By.XPath("//li/a[@href='/profile']"));

        public void GoToProfilePage()
        {
            ProfileMenu.Click();
        }

        public void GoToExploreJobsPage()
        {
            JobsMenu.Hover();
            ExploreJobsMenu.Click();
        }

    }
}
