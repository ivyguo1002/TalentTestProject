using API.TalentAPI;
using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Talent.Models;
using Talent.Pages;
using TechTalk.SpecFlow;

namespace Talent.Specflow.Steps
{
    [Binding, Scope(Feature = "Profile")]
    public class ProfileSteps
    {
        private PageFactory _pages;
        public ProfileSteps(PageFactory pages)
        {
            _pages = pages;
        }

        private const string TestDataPath = "TestData\\skills.json";
        private List<Skill> Skills => JsonDataHelper.ToObject<List<Skill>>(TestDataPath);
        private Skill NewSkill => Skills.Where(x => x.Key == "new").FirstOrDefault();
        private Skill UpdateSkill => Skills.Where(x => x.Key == "update").FirstOrDefault();

        [BeforeScenario]
        public void Before()
        {
            //API to clear up profile data
        }

        [Given(@"a talent user is on the profile page")]
        public void GivenATalentUserIsOnTheProfilePage()
        {
            _pages.Profile.Open();
        }
        
        [When(@"the user add a skill")]
        public void WhenTheUserAddASkill()
        {
            _pages.Profile.Skills.AddSkill(NewSkill);
      
        }
        
        [When(@"the user updates the skill")]
        public void WhenTheUserUpdatesTheSkill()
        {
            _pages.Profile.Skills.EditSkill(NewSkill, UpdateSkill);
        }
        
        [When(@"the user deletes the skill")]
        public void WhenTheUserDeletesTheSkill()
        {
            _pages.Profile.Skills.DeleteSkill(NewSkill);
        }
        
        [Then(@"the profile page displays the skill")]
        public void ThenTheProfilePageDisplaysTheSkill()
        {
            Assert.IsTrue(_pages.Profile.Skills.IsDisplayed(NewSkill));

        }

        [Given(@"post a skill through the skill api")]
        public void GivenPostASkillThroughTheSkillApi()
        {
            ProfileAPI.PostSkill(NewSkill);
        }

        [Then(@"the skill is added")]
        public void ThenTheSkillIsAdded()
        {
            Assert.IsTrue(_pages.Profile.Skills.IsAdded(NewSkill));
        }

        [Then(@"the skill is updated")]
        public void ThenTheSkillIsUpdated()
        {
            Assert.IsTrue(_pages.Profile.Skills.IsUpdated(UpdateSkill));
        }

        [Then(@"the skill is deleted")]
        public void ThenTheSkillIsDeleted()
        {
            Assert.IsTrue(_pages.Profile.Skills.IsDeleted(NewSkill));
        }

    }
}
