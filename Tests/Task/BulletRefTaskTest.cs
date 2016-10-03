using BulletML.Enums;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class BulletRefTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectBullets()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(2, TestUtils.Manager.Movers.Count);

            mover = TestUtils.Manager.Movers[1];
            Assert.AreEqual("test", mover.Label);
        }

        [Test]
        public void CorrectParams()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Find the task for the BulletRef
            var bulletRefTask = mover.FindTaskByLabel("test");
            Assert.IsNotNull(bulletRefTask);
        }

        [Test]
        public void CorrectParams1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Find the task for the BulletRef
            var bulletRefTask = mover.FindTaskByLabelAndName("test", NodeName.bullet);
            Assert.AreEqual(1, bulletRefTask.Params.Count);
        }

        [Test]
        public void CorrectParams3()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Find the task for the BulletRef
            var bulletRefTask = mover.FindTaskByLabelAndName("test", NodeName.bullet);
            Assert.AreEqual(15.0f, bulletRefTask.Params[0]);
        }

        [Test]
        public void FireTaskCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Find the task for the BulletRef
            var fireTask = mover.FindTaskByLabelAndName("testFire", NodeName.fire) as BulletML.Tasks.FireTask;
            Assert.IsNotNull(fireTask);
        }

        [Test]
        public void FireTaskCorrect1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Find the task for the BulletRef
            var fireTask = mover.FindTaskByLabelAndName("testFire", NodeName.fire) as BulletML.Tasks.FireTask;
            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.SpeedTask);
        }

        [Test]
        public void CorrectSpeedFromParam()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(2, TestUtils.Manager.Movers.Count);

            mover = TestUtils.Manager.Movers[1];
            Assert.AreEqual("test", mover.Label);
            Assert.AreEqual(15.0f, mover.Speed);
        }
    }
}