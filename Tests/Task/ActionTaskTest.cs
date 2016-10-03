using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class ActionTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            Assert.IsNotNull(mover.Tasks[0].Node);
            Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
        }

        [Test]
        public void RepeatOnce()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask myAction = mover.Tasks[0] as ActionTask;

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.AreEqual(1, testNode.RepeatNum(myAction, mover));
        }

        [Test]
        public void CorrectAction()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test]
        public void CorrectAction1()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
            Assert.IsTrue(myTask.ChildTasks[0] is ActionTask);
        }

        [Test]
        public void CorrectAction2()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;

            Assert.IsNotNull(testTask.Node);
            Assert.IsTrue(testTask.Node.Name == NodeName.action);
            Assert.AreEqual(testTask.Node.Label, "test");
        }

        [Test]
        public void RepeatNumInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
            Assert.AreEqual(0, testTask.RepeatNum);
        }

        [Test]
        public void RepeatNumMaxInitCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
            ActionNode actionNode = testTask.Node as ActionNode;

            Assert.AreEqual(1, actionNode.RepeatNum(testTask, mover));
        }

        [Test]
        public void RepeatNumMaxCorrect()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatMany.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
            Assert.IsNotNull(testTask);
        }

        [Test]
        public void RepeatNumMaxCorrect1()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRepeatMany.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
            ActionNode actionNode = testTask.Node as ActionNode;

            Assert.AreEqual(10, actionNode.RepeatNum(testTask, mover));
        }
    }
}