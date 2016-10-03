using BulletML;
using NUnit.Framework;
using System.IO;
using Tests.Utils;

namespace Tests
{
    [TestFixture()]
    public class XmlParsingTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void ValidateTestData()
        {
            // Get all XML test files
            foreach (var source in Directory.GetFiles(TestUtils.GetFilePath("Content"), "*.xml"))
            {
                // Load and validate every patterns
                var pattern = new BulletPattern();
                pattern.Parse(source);
            }
        }

        [Test]
        public void MakeSureNothingCrashes()
        {
            // Get all XML test files
            foreach (var source in Directory.GetFiles(TestUtils.GetFilePath("Content"), "*.xml"))
            {
                var pattern = new BulletPattern();
                pattern.Parse(source);

                // Fire in the hole
                TestUtils.Manager.Movers.Clear();
                var mover = (Mover)TestUtils.Manager.CreateBullet();
                mover.InitTopNode(pattern.RootNode);
            }
        }
    }
}