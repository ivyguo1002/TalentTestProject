using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Talent.Pages.PageFactory;

namespace Talent.Tests.Tests.Recruiter
{
    [TestFixture]
    public class JobsTest : RecruiterBaseTest
    {
        [Test]
        public void DisplayJobsStatus()
        {
            Jobs.Open();
            Assert.IsTrue(Jobs.IsJobStatusDisplayedWell());
            
        }
        [Test]
        public void ChangeJobStatus()
        {
            Jobs.Open();
            var oldJobStatus = Jobs.GetJobStatus();
            Jobs.ClickJobSwitch();
            var newJobStatus = Jobs.GetJobStatus();
            Assert.IsTrue(Jobs.IsJobStatusChanged(oldJobStatus, newJobStatus));
        }

    }
}
