using Framework.Config;
using Framework.Enums;
using Framework.Utils;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using System.Text;

namespace Framework.Selenium
{
    public static class Driver
    {
        [ThreadStatic] private static IWebDriver _driver;
        [ThreadStatic] public static WebDriverWait Wait;
        [ThreadStatic] public static WindowManager Window;
        public static IWebDriver Current => _driver ?? throw new NullReferenceException("");

        public static string Title => Current.Title;

        public static string Url => Current.Url;
        public static void Init()
        {
            _driver = WebDriverFactory.Build();
            Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(FW.Settings.Driver.DefaultWait));
            Window = new WindowManager();
            //Window.Maximize();
        }

        public static void Quit()
        {
            ReportHelper.LogTestStepInfo("Quit the web driver");
            if (Current != null)
            {
                Current.Quit();
            }
        }

        public static void GoToUrl(string url)
        {
            Current.Navigate().GoToUrl(url);
        }
        public static void Refresh()
        {
            Current.Navigate().Refresh();
        }

        public static string TakeScreenshot(string testName)
        {
            var screenshot = ((ITakesScreenshot)Current).GetScreenshot();
            var screenshotFilePath = FW.BaseDir + FW.Settings.Test.ScreenshotPath + testName + DateTime.Now.ToString("_MM_dd_yyyy_HH-mm") + ".png";
            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
            return screenshotFilePath;
        }

        public static void ExecuteScript(string script, params object[] args) => Current.ExecuteJavaScript(script, args);

        public static T ExecuteScript<T>(string script, params object[] args) => Current.ExecuteJavaScript<T>(script, args); 

        public static Element FindElement(By by, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Current, TimeSpan.FromSeconds(timeout));
                var element = wait.Until(driver => driver.FindElement(by));
                return new Element(element) {By = by};
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Exception in FindElement: element located by {by} not present within {timeout} seconds");
            }
        }

        public static List<Element> FindElements(By by, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Current, TimeSpan.FromSeconds(timeout));
                var elements = wait.Until(driver => driver.FindElements(by));
                var elementList = new List<Element>();
                foreach (var element in elements)
                {
                    elementList.Add(new Element(element));
                }
                return elementList;
            }
            catch (WebDriverTimeoutException)
            {

                throw new WebDriverException($"Exception in FindElements: elements located by {by} not present within {timeout} seconds");
            }
        }

        public static void WaitForPageLoaded(string title, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Current, TimeSpan.FromSeconds(timeout));
                wait.Until( driver => driver.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
              
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Exception in WaitForPageLoad:  the page title {title} not displayed within {timeout} seconds.");
            }
        }

        public static void ResetDriver() => ExecuteScript("window.localStorage.clear");
        public static bool HasSuccessMessage()
        {
            try
            {
                FindElement(By.CssSelector("div.ant-message-success"));
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public static string GetMessage()
        {
            var message = FindElement(By.CssSelector("div.ant-message span"));
            return message.Text;
        }

        public static void SelectDropDownList(string menuName)
        {
            var menu = Driver.FindElement(By.XPath("//li[contains(.,'" + menuName + "')]"));
            menu.Click();
        }
    }
}
