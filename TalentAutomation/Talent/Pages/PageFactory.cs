using System;
using System.Collections.Generic;
using System.Text;
using Talent.Pages.Dashboard;
using Talent.Pages.Jobs;
using Talent.Pages.Login;
using Talent.Pages.Profile;

namespace Talent.Pages
{
    public static class PageFactory
    {
        [ThreadStatic] public static LoginPage Login;
        [ThreadStatic] public static DashboardPage Dashboard;
        [ThreadStatic] public static ProfilePage Profile;
        [ThreadStatic] public static ExploreJobsPage ExploreJobs;
        [ThreadStatic] public static JobsPage Jobs;

        public static void Init()
        {
            Login = new LoginPage();
            Dashboard = new DashboardPage();
            Profile = new ProfilePage();
            ExploreJobs = new ExploreJobsPage();
            Jobs = new JobsPage();
        }
    }
}
