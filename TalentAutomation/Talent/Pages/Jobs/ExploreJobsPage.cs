using Framework.Selenium;
using Talent.Pages.Base;

namespace Talent.Pages.Jobs
{
    public class ExploreJobsPage : BasePage
    {
        public override string Title => "Explore Jobs - GTIO";
        public override string Url => "/jobs/explore";

        public ExploreJobsPage(Driver driver) : base(driver)
        {
        }
    }
}