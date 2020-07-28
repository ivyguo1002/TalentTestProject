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
        private static AventStack.ExtentReports.ExtentReports Extent { get; set; }

        [ThreadStatic] private static ExtentTest _currentTest;
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

        public static void LogTestStepInfo(string message)
        {
            _currentTest.Info(message);
        }

        public static void AddErrorLogToReport(Exception e)
        {
            _currentTest.Fail(e);
        }

        public static void AddTestMethodMetadataToReport(TestContext testContext, List<string> categories)
        {
            _currentTest = Extent.CreateTest(testContext.Test.MethodName);
            
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    _currentTest.AssignCategory(category);
                }
            }
        }

        public static void AddTestOutcomeToReport(TestContext testContext)
        {
            var result = testContext.Result.Outcome.Status;
            var testName = testContext.Test.MethodName;

            switch (result)
            {
                case TestStatus.Failed:
                    var screenshotFilePath = Driver.TakeScreenshot(testName);
                    _currentTest.Fail($"{testContext.Result.Message} {testContext.Result.StackTrace}")
                        .AddScreenCaptureFromPath(screenshotFilePath);
                    break;
                case TestStatus.Inconclusive:
                    break;
                case TestStatus.Passed:
                    _currentTest.Pass($"Test Passed: {testName}");
                    break;

                case TestStatus.Skipped:
                    _currentTest.Skip($"Test Skipped: {testName}");
                    break;

                default:
                    break;
            }
        }

        public static void Flush()
        {
            Extent.Flush();
        }

    }
}

