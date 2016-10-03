using BulletML;
using BulletML.Enums;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class TaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void OneAction()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");

            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.AreEqual(1, mover.Tasks.Count);
        }

        [Test]
        public void NoChildTasks()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.AreEqual(0, mover.Tasks[0].ChildTasks.Count);
        }

        [Test]
        public void NoParams()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.AreEqual(0, mover.Tasks[0].Params.Count);
        }

        [Test]
        public void NoOwner()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.IsNull(mover.Tasks[0].Owner);
        }

        [Test]
        public void CorrectNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.IsNotNull(mover.Tasks[0].Node);
        }

        [Test]
        public void NotFinished()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.IsFalse(mover.Tasks[0].Finished);
        }

        [Test]
        public void OkFinished()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.AreEqual(TaskRunStatus.End, mover.Tasks[0].Run(mover));
        }

        [Test]
        public void TaskFinishedFlag()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Tasks[0].Run(mover);

            Assert.IsTrue(mover.Tasks[0].Finished);
        }
    }
}