using BoDi;
using Framework.Config;
using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Talent.Pages;
using Talent.Services;
using TechTalk.SpecFlow;

namespace Talent.Specflow.Base
{
    [Binding]
    public class DriverInit
    {
        private IObjectContainer _objectContainer;
        public DriverInit(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            ReportHelper.AddScenarioInfo(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);
            var driver = new Driver();
            var pages = new PageFactory(driver);

            _objectContainer.RegisterInstanceAs(driver);
            _objectContainer.RegisterInstanceAs(pages);

            driver.GoToUrl(FW.Settings.Test.BaseUrl);
        }

        

    }
}
