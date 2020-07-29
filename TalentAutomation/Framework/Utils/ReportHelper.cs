using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Framework.Config;
using Framework.Selenium;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Framework.Utils
{
    public class ReportHelper
    {
        [ThreadStatic] private static ExtentTest _scenario;
        [ThreadStatic] private static ExtentTest _step;
        public static AventStack.ExtentReports.ExtentReports Extent { get; set; }
        public static string CreateReportDirectory()
        {
            var reportFolder = FW.BaseDir + FW.Settings.Test.ReportPath;

            return Directory.CreateDirectory(reportFolder + "Report" +
                DateTime.Now.ToString("_MM_dd_yyyy_HH-mm") + "\\").ToString();
        }

        public static void StartReporter()
        {
            var reportDirectory = CreateReportDirectory();
            var htmlReporter = new ExtentHtmlReporter(reportDirectory);
            Extent = new AventStack.ExtentReports.ExtentReports();
            Extent.AttachReporter(htmlReporter);
        }

        public static void AddScenarioInfo(string featureTitle, string scenarioTitle)
        {
            _scenario = ReportHelper.Extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(featureTitle)

            .CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(scenarioTitle);
        }

        public static void Flush()
        {
            Extent.Flush();
        }

        public static void AddStepInfo(string keyword, string text)
        {
            switch (keyword)
            {
                case "Given":
                    _step = _scenario.CreateNode<Given>(text);
                    break;
                case "And":
                    _step = _scenario.CreateNode<And>(text);
                    break;
                case "When":
                    _step = _scenario.CreateNode<When>(text);
                    break;
                case "Then":
                    _step = _scenario.CreateNode<Then>(text);
                    break;

                default:
                    throw (new ArgumentOutOfRangeException("The step isn't defined"));
            }
        }

        public static void AddTestError(Exception error, string screenshotPath)
        {
            _step.Fail($"{error.Message} {error.StackTrace}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }

    }
}

