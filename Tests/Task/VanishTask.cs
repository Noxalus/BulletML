using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class VanishTask
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void VanishTaskTest()
        {
            var filename = TestUtils.GetFilePath(@"Content\Vanish.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(0, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void NestedVanish()
        {
            var filename = TestUtils.GetFilePath(@"Content\NestedVanish.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(0, TestUtils.Manager.Movers.Count);
        }
    }
}