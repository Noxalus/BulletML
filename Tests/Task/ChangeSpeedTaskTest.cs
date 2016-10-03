using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class ChangeSpeedTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectSpeed()
        {
            var filename = TestUtils.GetFilePath(@"Content\ChangeSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            Assert.AreEqual(0, mover.Speed);
        }

        [Test]
        public void CorrectSpeed1()
        {
            var filename = TestUtils.GetFilePath(@"Content\ChangeSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(1, mover.Speed);
        }

        [Test]
        public void ChangeSpeedAbs()
        {
            var filename = TestUtils.GetFilePath(@"Content\ChangeSpeedAbs.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            mover.Speed = 110;
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(110, mover.Speed);
            TestUtils.Manager.Update();
            Assert.AreEqual(100, mover.Speed);

            for (int i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            Assert.AreEqual(10, mover.Speed);
        }

        [Test]
        public void ChangeSpeedRel()
        {
            var filename = TestUtils.GetFilePath(@"Content\ChangeSpeedRel.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(100, mover.Speed);
            TestUtils.Manager.Update();
            Assert.AreEqual(101, mover.Speed);

            for (int i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            Assert.AreEqual(110, mover.Speed);
        }

        [Test]
        public void ChangeSpeedSeq()
        {
            var filename = TestUtils.GetFilePath(@"Content\ChangeSpeedSeq.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(100, mover.Speed);
            TestUtils.Manager.Update();
            Assert.AreEqual(110, mover.Speed);

            for (int i = 0; i < 10; i++)
            {
                TestUtils.Manager.Update();
            }

            Assert.AreEqual(200, mover.Speed);
        }
    }
}