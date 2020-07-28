using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Talent.Services;
using Talent.Tests.Base;

namespace Talent.Tests.Tests.Recruiter
{
    public class RecruiterBaseTest : BaseTest
    {
        [SetUp]
        public override void BeforeEachMethod()
        {
            base.BeforeEachMethod();
            TokenHelper.SetToken(TokenHelper.RecruiterToken);
        }
    }
}
