using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Config
{
    public class Setting
    {
        public TestSetting Test { get; set; }
        public WebDriverSetting Driver { get; set; }
        public ChromeSetting Chrome { get; set; }
        public FirefoxSetting Firefox { get; set; }
    }
    public class TestSetting
    {
        public string BaseUrl { get; set; }

        public string IdentityAPI { get; set; }

        public string TalentAPI { get; set; }

        public string ScreenshotPath { get; set; }
        public string ReportPath { get; set; }
    }

    public class WebDriverSetting
    {
        public string WebDriverType { get; set; }
        public string BrowserType { get; set; }
        public int DefaultWait { get; set; }
    }

    public class ChromeSetting
    {
        public string PlatForm { get; set; }
        public string BrowserVersion { get; set; }
    }

    public class FirefoxSetting
    {
        public string PlatForm { get; set; }
        public string BrowserVersion { get; set; }

    }
}
