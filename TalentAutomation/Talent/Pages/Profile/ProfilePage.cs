using Framework.Selenium;
using Talent.Models;
using Talent.Pages.Base;

namespace Talent.Pages.Profile
{
    public class ProfilePage : BasePage
    {
        public override string Url => "/profile";
        public override string Title => "Profile";

        public Skills Skills;
        public ProfilePage(Driver driver) : base(driver)
        {
            Skills = new Skills(driver);
        }
    }
}