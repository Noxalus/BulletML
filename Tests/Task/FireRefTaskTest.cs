using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class FireRefTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectBullets()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(2, TestUtils.Manager.Movers.Count);

            mover = TestUtils.Manager.Movers[1];
            Assert.AreEqual("testBullet", mover.Label);
        }

        [Test]
        public void CorrectSpeedFromParam()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(2, TestUtils.Manager.Movers.Count);

            mover = TestUtils.Manager.Movers[1];
            Assert.AreEqual("testBullet", mover.Label);
            Assert.AreEqual(15.0f, mover.Speed);
        }
    }
}