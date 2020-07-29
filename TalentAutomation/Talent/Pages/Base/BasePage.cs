using Framework.Config;
using Framework.Selenium;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Talent.Pages.Base
{
    public abstract class BasePage
    {
        public Driver Driver { get; set; }
        public string BaseUrl => FW.Settings.Test.BaseUrl;
        public virtual string Url { get; set; }
        public virtual string Title { get; set; }

        public BasePage(Driver driver)
        {
            Driver = driver;
        }

        public void Open()
        {
            //ReportHelper.LogTestStepInfo($"Navigate to {Title} page: {BaseUrl}{Url}");
            Driver.GoToUrl(BaseUrl + Url);
            Driver.WaitForPageLoaded(Title);
        }

        public bool IsLoaded()
        {
            //ReportHelper.LogTestStepInfo($"Check if the {Title} page is loaded successully");
            try
            {
                Driver.WaitForPageLoaded(Title);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
