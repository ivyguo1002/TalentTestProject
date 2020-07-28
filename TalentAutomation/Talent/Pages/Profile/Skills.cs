using Framework.Selenium;
using Framework.Utils;
using NPOI.SS.Formula.Functions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Talent.Enums;
using Talent.Models;

namespace Talent.Pages.Profile
{
    public class Skills
    {
        private Element SkillsSection => Driver.FindElement(By.XPath("//div[contains(@class,'ant-collapse-item')][contains(., 'Skills')]"));
        private Element SkillsHeader => SkillsSection.FindElement(By.CssSelector(".ant-collapse-header"));
        private Element AddNewSkillBtn => SkillsSection.FindElement(By.CssSelector(".ant-btn-dashed"));
        private Element NameTxtBox => SkillsSection.FindElement(By.Id("name"));
        private Element LevelComboBox => SkillsSection.FindElement(By.Id("level"));
        private Element CheckIcon => SkillsSection.FindElement(By.CssSelector(".anticon-check"));

        private Element SkillsTable => SkillsSection.FindElement(By.TagName("table"));
        private Element EditIcon(int rowIndex) => SkillsSection.FindElements(By.CssSelector(".anticon-edit"))[rowIndex];

        private Element DeleteIcon(int rowIndex) => SkillsSection.FindElements(By.CssSelector(".anticon-delete"))[rowIndex];

        private Element PopoverConfirmBtn => Driver.FindElement(By.CssSelector(".ant-popover-buttons button.ant-btn-primary"));

        private Element CloseIcon => SkillsSection.FindElement(By.CssSelector(".anticon-close"));

        public bool? IsUpdated(Skill skill)
        {
            return Driver.HasSuccessMessage();
            //todo: Add assertion for message text
        }

        public void AddSkill(Skill skill)
        {
            ReportHelper.LogTestStepInfo("Add a new skill");

            SkillsHeader.ExpandHeader();
            AddNewSkillBtn.Click();
            NameTxtBox.SendKeys(skill.Name);
            LevelComboBox.Click();
            SelectLevel(skill.Level);
            CheckIcon.Click();
        }

        public bool IsAdded(Skill skill)
        {
            ReportHelper.LogTestStepInfo("Check if the skill has been added");
            return Driver.HasSuccessMessage();
            //todo: Add assertion for message text
        }

        public void EditSkill(Skill skill, Skill updatedSkill)
        {
            ReportHelper.LogTestStepInfo("Edit the skill");
            SkillsHeader.ExpandHeader();
            int rowIndex = GetRowIndex(skill);
            EditIcon(rowIndex).Click();
            NameTxtBox.SendKeys(updatedSkill.Name);
            LevelComboBox.Click();
            SelectLevel(updatedSkill.Level);
            CheckIcon.Click();
        }

        public void DeleteSkill(Skill skill)
        {
            ReportHelper.LogTestStepInfo("Delete the skill");
            SkillsHeader.ExpandHeader();
            var rowIndex = GetRowIndex(skill);
            DeleteIcon(rowIndex).Click();
            PopoverConfirmBtn.Click();
        }

        public bool IsDeleted(Skill skill)
        {
            return Driver.HasSuccessMessage();
        }

        public bool IsDisplayed(Skill skill)
        {
            ReportHelper.LogTestStepInfo($"Check if the skill is displayed");
            SkillsHeader.ExpandHeader();
            int rowIndex = GetRowIndex(skill);
            if (rowIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SelectLevel(string level)
        {
            if (!Enum.IsDefined(typeof(Level), level))
            {
                throw new ArgumentException($"{level} is invalid. Choose a valid Level");

            }
            Driver.SelectDropDownList(level);
        }
        private int GetRowIndex(Skill skill)
        {
            int rowIndex = SkillsTable.SearchDataFromTable(
                            new Dictionary<string, string>
                            {
                    { "Skill", skill.Name},
                    { "Level", skill.Level}
                            }
                            );
            if (rowIndex == -1)
            {
                throw new ArgumentException("The table data doesn't exist");
            }

            return rowIndex;
        }
    }
}