using NUnit.Framework;
using System;
using Talent.Pages;
using TechTalk.SpecFlow;

namespace Talent.Specflow.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private PageFactory _pages;
        public DashboardSteps(PageFactory pages)
        {
            _pages = pages;
        }

        [Given(@"a talent user is on Dashboard Page")]
        public void GivenATalentUserIsOnDashboardPage()
        {
            _pages.Dashboard.Open();
        }
        
        [When(@"the user clicks on Profile menu")]
        public void WhenTheUserClicksOnProfileMenu()
        {
            _pages.Dashboard.GlobalHeader.GoToProfilePage();
        }
        
        [Then(@"the user is navigated to profile page")]
        public void ThenTheUserIsNavigatedToPage()
        {
            Assert.That(_pages.Profile.IsLoaded(), Is.True);
        }

        [When(@"the user clicks on Explore Jobs menu")]
        public void WhenTheUserClicksOnExploreJobsMenu()
        {
            _pages.Dashboard.GlobalHeader.GoToExploreJobsPage();
           
        }

        [Then(@"the user is navigated to Explore Jobs page")]
        public void ThenTheUserIsNavigatedToExploreJobsPage()
        {
            Assert.That(_pages.ExploreJobs.IsLoaded(), Is.True);
        }

    }
}
