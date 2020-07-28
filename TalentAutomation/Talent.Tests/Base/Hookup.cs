using Framework.Config;
using Framework.Utils;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(2)]
namespace Talent.Tests
{
    [SetUpFixture]
    public class Hookup
    {
        [OneTimeSetUp]
        public void AssemblySetup()
        {
            FW.SetConfig();

            ReportHelper.StartReporter();

        }

        [OneTimeTearDown]
        public void AssemblyTearDown()
        {
            ReportHelper.Flush();
         
        }
    }
}
