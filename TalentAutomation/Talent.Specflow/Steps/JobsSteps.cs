using NUnit.Framework;
using System;
using Talent.Models;
using Talent.Pages;
using TechTalk.SpecFlow;

namespace Talent.Specflow.Steps
{
    [Binding]
    public class JobsSteps
    {
        private PageFactory _pages;
        private ScenarioContext _scenarioContext;
        public JobsSteps(PageFactory pages, ScenarioContext scenarioContext)
        {
            _pages = pages;
            _scenarioContext = scenarioContext;
        }

        [Given(@"a recruiter user is on the Jobs page")]
        public void GivenARecruiterUserIsOnTheJobsPage()
        {
            _pages.Jobs.Open();
            _scenarioContext["oldJobStatus"] = _pages.Jobs.GetJobStatus();
        }
        
        [When(@"the user click the job switch button on the first job card")]
        public void WhenTheUserClickTheJobSwitchButtonOnTheFirstJobCard()
        {
            _pages.Jobs.ClickJobSwitch();
        }
        
        [Then(@"The first job status is updated")]
        public void ThenTheFirstJobStatusIsUpdated()
        {
            var newJobStatus = _pages.Jobs.GetJobStatus();
            Assert.IsTrue(_pages.Jobs.IsJobStatusChanged((JobStatus)_scenarioContext["oldJobStatus"], newJobStatus));
        }
    }
}
