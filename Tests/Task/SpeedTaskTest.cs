using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class SpeedTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.IsNotNull(mover.Tasks[0].Node);
            Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
        }

        [Test]
        public void RepeatOnce()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask myAction = mover.Tasks[0] as ActionTask;

            ActionNode testNode = TestUtils.Pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.AreEqual(1, testNode.RepeatNum(myAction, mover));
        }

        [Test]
        public void CorrectAction()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test]
        public void CorrectAction1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
            Assert.IsTrue(myTask.ChildTasks[0] is BulletML.Tasks.FireTask);
        }

        [Test]
        public void CorrectAction2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(testTask.Node);
            Assert.IsTrue(testTask.Node.Name == NodeName.fire);
        }

        [Test]
        public void NoSubTasks()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.AreEqual(1, testTask.ChildTasks.Count);
        }

        [Test]
        public void FireSpeedInitInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(testTask.SpeedTask);
        }

        [Test]
        public void FireSpeedInitInitCorrect1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsTrue(testTask.SpeedTask is SpeedTask);
        }

        [Test]
        public void FireSpeedTaskValue()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;

            Assert.IsNotNull(speedTask.Node);
        }

        [Test]
        public void FireSpeedTaskValue1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;

            Assert.IsTrue(speedTask.Node is SpeedNode);
        }

        [Test]
        public void FireSpeedTaskValue2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            Assert.IsNotNull(speedNode);
        }

        [Test]
        public void FireSpeedTaskValue3()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            Assert.AreEqual(5.0f, speedNode.GetValue(speedTask));
        }

        [Test]
        public void FireSpeedInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.AreEqual(5.0f, testTask.FireSpeed);
        }

        [Test]
        public void FireSpeedInitCorrect1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeedBulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.AreEqual(5.0f, testTask.FireSpeed);
        }

        [Test]
        public void BulletSpeedInitInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsNotNull(testTask.SpeedTask);
        }

        [Test]
        public void BulletSpeedInitInitCorrect1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.IsTrue(testTask.SpeedTask is SpeedTask);
        }

        [Test]
        public void BulletSpeedTaskValue()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;

            Assert.IsNotNull(speedTask.Node);
        }

        [Test]
        public void BulletSpeedTaskValue1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;

            Assert.IsTrue(speedTask.Node is SpeedNode);
        }

        [Test]
        public void BulletSpeedTaskValue2()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            Assert.IsNotNull(speedNode);
        }

        [Test]
        public void BulletSpeedTaskValue3()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;
            SpeedTask speedTask = testTask.SpeedTask as SpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            Assert.AreEqual(10.0f, speedNode.GetValue(speedTask));
        }

        [Test]
        public void BulletSpeedInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletSpeed.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            BulletML.Tasks.FireTask testTask = myTask.ChildTasks[0] as BulletML.Tasks.FireTask;

            Assert.AreEqual(10.0f, testTask.FireSpeed);
        }
    }
}