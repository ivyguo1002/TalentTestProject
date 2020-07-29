using Framework.Selenium;
using OpenQA.Selenium;
using Talent.Pages.Base;

namespace Talent.Pages.Dashboard
{
    public partial class DashboardPage : BasePage
    {
        public override string Title => "Dashboard";
        public override string Url => "/dashboard";

        public GlobalHeader GlobalHeader { get; set; }

        public DashboardPage(Driver driver) : base(driver)
        {
            GlobalHeader = new GlobalHeader(driver);
        }
    }
}