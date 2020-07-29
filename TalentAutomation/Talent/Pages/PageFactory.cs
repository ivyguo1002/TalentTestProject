using Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Talent.Pages.Dashboard;
using Talent.Pages.Jobs;
using Talent.Pages.Login;
using Talent.Pages.Profile;

namespace Talent.Pages
{
    public class PageFactory
    {
        public LoginPage Login { get; set; }
        public DashboardPage Dashboard { get; set; }
        public ProfilePage Profile { get; set; }
        public ExploreJobsPage ExploreJobs { get; set; }
        public JobsPage Jobs { get; set; }

        public PageFactory(Driver driver)
        {
            Login = new LoginPage(driver);
            Dashboard = new DashboardPage(driver);
            Profile = new ProfilePage(driver);
            ExploreJobs = new ExploreJobsPage(driver);
            Jobs = new JobsPage(driver);
        }

    }
}
