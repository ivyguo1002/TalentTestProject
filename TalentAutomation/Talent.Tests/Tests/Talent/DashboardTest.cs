using NUnit.Framework;
using Talent.Pages;
using Talent.Pages.Dashboard;
using Talent.Tests.Base;
using static Talent.Pages.PageFactory;

namespace Talent.Tests.Tests.Talent
{
    [Category("Dashboard")]
    [TestFixture]
    public class DashboardTest : TalentBaseTest
    {
        [Test]
        public void NavigateToProfilePage()
        {
            Dashboard.Open();
            Dashboard.GlobalHeader.GoToProfilePage();
            Assert.That(Profile.IsLoaded(), Is.True);
        }

        [Test]
        public void NavigateToJobWatchListPage()
        {
            Dashboard.Open();
            Dashboard.GlobalHeader.GoToExploreJobsPage();
            Assert.That(ExploreJobs.IsLoaded(), Is.True);
        }
    }
}
