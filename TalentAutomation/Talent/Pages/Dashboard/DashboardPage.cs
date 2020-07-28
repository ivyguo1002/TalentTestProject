using Framework.Selenium;
using OpenQA.Selenium;
using Talent.Pages.Base;

namespace Talent.Pages.Dashboard
{
    public partial class DashboardPage : BasePage
    {
        public override string Title => "Dashboard";
        public override string Url => "/dashboard";

        public GlobalHeader GlobalHeader;

        public DashboardPage()
        {
            GlobalHeader = new GlobalHeader();
        }
    }
}