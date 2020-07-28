using AventStack.ExtentReports.Gherkin;
using Framework.Selenium;
using Framework.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Talent.Models;
using Talent.Pages.Base;

namespace Talent.Pages.Jobs
{
    public class JobsPage : BasePage
    {
        private Filter filter;

        public override string Title => "Jobs - GTIO";
        public override string Url => "/jobs";
        private Element FirstJobInList => Driver.FindElement(By.CssSelector("div.ant-card-body"), 15);
        private Element FirstJobTitle => FirstJobInList.FindElement(By.CssSelector("h4.ant-list-item-meta-title span:first-child"));
        private Element FirstJobStatus => FirstJobInList.FindElement(By.CssSelector("h4.ant-list-item-meta-title span.ant-tag"));
        private Element FirstJobDesc => FirstJobInList.FindElement(By.CssSelector("div.antd-pro-app-src-pages-job-common-job-list-jobSummary div"));
        private Element FirstJobSwitch => FirstJobInList.FindElement(By.XPath("//ul[@class='ant-card-actions']/li[2]/span/div/span"));
        public Filter Filter { get; set; }
        private JobStatus JobStatusAttr => GetJobStatus();
        private JobStatus ActiveStatus = new JobStatus { JobStatusText = "Active", JobStatusColour = Color.FromArgb(16, 142, 233) };
        private JobStatus ClosedStatus = new JobStatus { JobStatusText = "Closed" , JobStatusColour = Color.FromName("gray")};
        

        public JobsPage()
        {
            Filter = new Filter();
        }

        public JobStatus GetJobStatus()
        {
            string color = FirstJobStatus.GetCssValue("background-color");
            return new JobStatus
            {
                JobStatusText = FirstJobStatus.Text,
                JobStatusColour = Extensions.ParseFromStr(color)
            };
        }

      
        public bool IsJobStatusDisplayedWell()
        {
            ReportHelper.LogTestStepInfo("Check if the job status is displayed well: the colour should match the text");
            switch (JobStatusAttr.JobStatusText)
            {
                case "Closed":
                    return JobStatusAttr.Equals(ClosedStatus);
                case "Active":
                    return JobStatusAttr.Equals(ActiveStatus);
                default:
                    throw new ArgumentOutOfRangeException($"The job status {JobStatusAttr.JobStatusText} is invalid");
            }
        }
        public void ClickJobSwitch()
        {
            ReportHelper.LogTestStepInfo("Click the job status switch");
            FirstJobSwitch.Click();
        }

        public bool IsJobStatusChanged(JobStatus oldJobStatus, JobStatus newJobStatus)
        {
            ReportHelper.LogTestStepInfo("Check if the job status is changed");
            switch (oldJobStatus.JobStatusText)
            {
                case "Closed":
                    return newJobStatus.Equals(ActiveStatus);
                  
                case "Active":
                    return newJobStatus.Equals(ClosedStatus);
                   
                default:
                    throw new ArgumentOutOfRangeException($"The job status {JobStatusAttr.JobStatusText} is invalid");
            }

        }

       
    }
}
