using API.TalentAPI;
using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Talent.Models;
using Talent.Pages;
using Talent.Pages.Profile;
using Talent.Tests.Base;
using static Talent.Pages.PageFactory;

namespace Talent.Tests.Tests.Talent
{
    [TestFixture]
    public class ProfileTest : TalentBaseTest
    {
        private const string TestDataPath = "TestData\\skills.json";
        private List<Skill> Skills => JsonDataHelper.ToObject<List<Skill>>(TestDataPath);
        private Skill NewSkill => Skills.Where(x => x.Key == "new").FirstOrDefault();

        private Skill UpdateSkill => Skills.Where(x => x.Key == "update").FirstOrDefault();

        [SetUp]
        public void BeforeEach()
        {
            //ProfileAPI.ResetProfile();
            //todo: BadRequest. Need to check the valid input
        }

        [Test]
        public void DisplaySkill()
        {
            ProfileAPI.PostSkill(NewSkill);
            Profile.Open();
            Assert.IsTrue(Profile.Skills.IsDisplayed(NewSkill));
        }

        [Test]
        public void AddNewSkill()
        {
            Profile.Open();
            Profile.Skills.AddSkill(NewSkill);
            Assert.IsTrue(Profile.Skills.IsAdded(NewSkill));
        }

        [Test]
        public void EditSkill()
        {
            ProfileAPI.PostSkill(NewSkill);
            Profile.Open();
            Profile.Skills.EditSkill(NewSkill, UpdateSkill);
            Assert.IsTrue(Profile.Skills.IsUpdated(UpdateSkill));
        }
        [Test]
        public void DeleteSkill()
        {
            ProfileAPI.PostSkill(NewSkill);
            Profile.Open();
            Profile.Skills.DeleteSkill(NewSkill);
            Assert.IsTrue(Profile.Skills.IsDeleted(NewSkill));
        }

        [TearDown]
        public void ResetProfile()
        {

            //ProfileAPI.DeleteSkill(_skillId);
            //ProfileAPI.ResetProfile();
            //todo: BadRequest. Need to check the valid input

        }
    }
}
