using Framework.Config;
using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Talent.Pages;

namespace Talent.Tests.Base
{
    public abstract class BaseTest
    {
        [SetUp]
        public virtual void BeforeEachMethod()
        {
            var categories = GetCategories();
            ReportHelper.AddTestMethodMetadataToReport(TestContext.CurrentContext, categories);

            Driver.Init();

            PageFactory.Init();

            Driver.GoToUrl(FW.Settings.Test.BaseUrl);
        }

        [TearDown]
        public virtual void AfterEachMethod()
        {
            ReportHelper.AddTestOutcomeToReport(TestContext.CurrentContext);

            Driver.Quit();
        }

        private List<string> GetCategories()
        {
            var classTestCategoryAttributes = GetType().GetCustomAttributes<CategoryAttribute>(true);
            var methodTestCategoryAttributes = GetType().GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttributes<CategoryAttribute>(true);

            var attributes = methodTestCategoryAttributes.Concat(classTestCategoryAttributes);
            var testCategories = new List<string>();
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    testCategories.Add(attribute.Name);
                }
            }
            return testCategories;

        }



    }
}