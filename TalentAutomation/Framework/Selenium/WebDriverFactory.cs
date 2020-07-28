using Framework.Config;
using Framework.Enums;
using Framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Selenium
{
    public class WebDriverFactory
    {
        private const string RemoteWebDriverHub = "http://localhost:4444/wd/hub";
        private const string SauceLabWebDriverHub = "https://ondemand.saucelabs.com:443/wd/hub";
        public static IWebDriver Build()
        {
            if (!Enum.TryParse<WebDriverType>(FW.Settings.Driver.WebDriverType, out WebDriverType webDriverType))
            {
                throw new ArgumentException($"webdrivertype is invalid. Choose 'local', 'remote' or 'sauce'");
            }

            if (!Enum.TryParse<BrowserType>(FW.Settings.Driver.BrowserType, out BrowserType browserType))
            {
                throw new ArgumentException($"browser isn't supported");
            }

            ReportHelper.LogTestStepInfo($"Build {webDriverType} {browserType} Web Driver");

            switch (webDriverType)
            {
                case WebDriverType.Local:
                    return BuildLocalDriver(browserType);
                    
                case WebDriverType.Remote:
                    return BuildRemoteDriver(browserType);
                    
                case WebDriverType.Sauce:
                    return BuildSauceDriver(browserType);
                    
                default:
                    throw new ArgumentOutOfRangeException("The web driver type isn't supported");

            }
        }

        private static IWebDriver BuildLocalDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    return new ChromeDriver(chromeOptions);
               
                case BrowserType.Firefox:
                    var profile = new FirefoxProfile();
                    profile.SetPreference("intl.accept_languages", "en,en-US");
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.Profile = profile;
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    return new FirefoxDriver(firefoxOptions);
                    
                default:
                    throw new ArgumentOutOfRangeException("The browser type isn't supported");

            }
        }

        private static IWebDriver BuildRemoteDriver(BrowserType browserType)
        {
            var remoteWebDriverUrl = new Uri(RemoteWebDriverHub);

            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    return new RemoteWebDriver(remoteWebDriverUrl, chromeOptions);
              
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    return new RemoteWebDriver(remoteWebDriverUrl, firefoxOptions);

                default:
                    throw new ArgumentOutOfRangeException("The browser type isn't supported");
            }
        }

        private static IWebDriver BuildSauceDriver(BrowserType browserType)
        {
            var sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME");
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY");

            var sauceLabUrl = new Uri(SauceLabWebDriverHub);

            /*
              * In this section, we will configure our test to run on some specific
              * browser/os combination in Sauce Labs
              */
            var sauceOptions = new Dictionary<string, object>()
            {
                ["username"] = sauceUserName,
                ["accessKey"] = sauceAccessKey,
            };

            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeCaps = FW.Settings.Chrome;
                    var chromeOptions = new ChromeOptions()
                    {
                        UseSpecCompliantProtocol = true,
                        PlatformName = chromeCaps.PlatForm,
                        BrowserVersion = chromeCaps.BrowserVersion
                    };

                    chromeOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);
                    return new RemoteWebDriver(sauceLabUrl, chromeOptions);

                case BrowserType.Firefox:
                    var firefoxCaps = FW.Settings.Firefox;
                    var firefoxOptions = new FirefoxOptions()
                    {
                        PlatformName = firefoxCaps.PlatForm,
                        BrowserVersion = firefoxCaps.BrowserVersion
                    };

                    firefoxOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);

                    return new RemoteWebDriver(sauceLabUrl, firefoxOptions);

                default:
                    throw new ArgumentOutOfRangeException("The browser type isn't supported");
            }
        }
    }
}
