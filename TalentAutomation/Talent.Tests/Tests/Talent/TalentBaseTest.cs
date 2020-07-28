using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Talent.Services;
using Talent.Tests.Base;

namespace Talent.Tests.Tests.Talent
{
    public class TalentBaseTest : BaseTest
    {
        [SetUp]
        public override void BeforeEachMethod()
        {
            base.BeforeEachMethod();
            TokenHelper.SetToken(TokenHelper.TalentToken);
        }
    }
}
