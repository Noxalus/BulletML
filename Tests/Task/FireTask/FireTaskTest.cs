using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task.FireTask
{
    [TestFixture()]
    [Category("TaskTest")]
    [Category("FireTaskTest")]
    public class FireTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.IsNotNull(mover.Tasks[0].Node);
            Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
        }

        [Test]
        public void RepeatOnce()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myAction = mover.Tasks[0] as ActionTask;

            var topNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.IsNotNull(topNode);
            Assert.AreEqual(1, topNode.RepeatNum(myAction, mover));
        }

        [Test]
        public void CorrectAction()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test]
        public void CorrectAction1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myTask = mover.Tasks[0];

            Assert.AreEqual(1, myTask.ChildTasks.Count);
            Assert.IsTrue(myTask.ChildTasks[0] is BulletML.Tasks.FireTask);
        }

        [Test]
        public void CorrectAction2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.Node);
            Assert.IsTrue(fireTask.Node.Name == NodeName.fire);
            Assert.AreEqual("test", fireTask.Node.Label);
        }

        [Test]
        public void NoSubTasks()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(1, fireTask.ChildTasks.Count);
        }

        [Test]
        public void NoSubTasks1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeedBulletSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(1, fireTask.ChildTasks.Count);
        }

        [Test]
        public void FireDirectionInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            float direction = MathHelper.ToDegrees(fireTask.FireDirection);
            Assert.AreEqual(direction, 180.0f);
        }

        [Test]
        public void FireDirectionInitCorrect1()
        {
            TestUtils.Player.Position.Y = -100.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            float direction = MathHelper.ToDegrees(fireTask.FireDirection);
            Assert.AreEqual(direction, 0.0f);
        }

        [Test]
        public void FireDirectionInitCorrect2()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            float direction = MathHelper.ToDegrees(fireTask.FireDirection);
            Assert.AreEqual(direction, 90.0f);
        }

        [Test]
        public void FireDirectionInitCorrect3()
        {
            TestUtils.Player.Position.X = -100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            float direction = MathHelper.ToDegrees(fireTask.FireDirection);
            Assert.AreEqual(-90f, direction);
        }

        [Test]
        public void FireSpeedInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var task = mover.Tasks[0];
            var fireTask = task.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(0f, fireTask.FireSpeed);
        }

        [Test]
        public void FireInitialRunInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
        }

        [Test]
        public void FireBulletRefInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.BulletTask);
        }

        [Test]
        public void FireInitDirInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNull(fireTask.DirectionTask);
        }

        [Test]
        public void FireSpeedInitInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNull(fireTask.SpeedTask);
        }

        [Test]
        public void FireDirSeqInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNull(fireTask.DirectionTask);
        }

        [Test]
        public void FireDirectionSequence()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionSequence2.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.AreEqual(1, mover.Tasks.Count);
            Assert.AreEqual(2, mover.Tasks[0].ChildTasks.Count);

            TestUtils.Manager.Update();

            Assert.AreEqual(3, TestUtils.Manager.Movers.Count);

            var bullet1 = TestUtils.Manager.Movers[1] as Bullet;
            var bullet2 = TestUtils.Manager.Movers[2] as Bullet;

            Assert.IsNotNull(bullet1);
            Assert.IsNotNull(bullet2);

            Assert.AreEqual(90f, MathHelper.ToDegrees(bullet1.Direction));
            Assert.AreEqual(MathHelper.ToDegrees(bullet1.Direction) + 10f, MathHelper.ToDegrees(bullet2.Direction));
        }

        [Test]
        public void FireSpeedSeqInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNull(fireTask.DirectionTask);
        }

        [Test]
        public void FoundBullet()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.BulletTask);
        }

        [Test]
        public void FoundBulletNoSubTasks()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            var myTask = mover.Tasks[0];
            var fireTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(1, fireTask.ChildTasks.Count);
        }
    }
}