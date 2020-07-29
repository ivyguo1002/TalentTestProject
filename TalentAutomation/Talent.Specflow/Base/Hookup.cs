using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using Framework.Config;
using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Talent.Pages;
using Talent.Services;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]
namespace Talent.Specflow
{
    [Binding]
    public class Hookup
    {
        private Driver _driver;
        public Hookup(Driver driver)
        {
            _driver = driver;
        }
       
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            FW.SetConfig();

            ReportHelper.StartReporter();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportHelper.Flush();
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string keyword = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string text = scenarioContext.StepContext.StepInfo.Text;

            ReportHelper.AddStepInfo(keyword, text);


            var error = scenarioContext.TestError;

            if (error != null)
            {
                string screenshotPath = _driver.TakeScreenshot(scenarioContext.ScenarioInfo.Title);
                ReportHelper.AddTestError(error, screenshotPath);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }

        [BeforeScenario("talent", Order = 2)]
        public void SignInAsTalent()
        {
            TokenHelper.SetToken(TokenHelper.TalentToken, _driver);
        }

        [BeforeScenario("recruiter", Order = 2)]
        public void SignInAsRecruiter()
        {
            TokenHelper.SetToken(TokenHelper.RecruiterToken, _driver);
        }

    }
}
