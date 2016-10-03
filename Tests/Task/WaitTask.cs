using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class WaitTask
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void WaitOneTaskTest()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitOne.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitOneTaskTest1()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitOne.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitOneTaskTest2()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitOne.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();
            TestUtils.Manager.Update();

            Assert.AreEqual(0, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitZeroTaskTest()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitZero.xml");
            TestUtils.Pattern.Parse(filename);

            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(0, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitTwoTaskTest()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitTwo.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitTwoTaskTest1()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitTwo.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitTwoTaskTest2()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitTwo.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();
            TestUtils.Manager.Update();

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void WaitTwoTaskTest3()
        {
            var filename = TestUtils.GetFilePath(@"Content\WaitTwo.xml");
            TestUtils.Pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();
            TestUtils.Manager.Update();
            TestUtils.Manager.Update();

            Assert.AreEqual(0, TestUtils.Manager.Movers.Count);
        }
    }
}