using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    [TestFixture()]
    public class AllRoundXmlTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void TestOneTop()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);

            ActionNode testNode = TestUtils.Pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.IsNotNull(testNode);
        }

        [Test]
        public void TestNoRepeatNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);

            ActionNode testNode = TestUtils.Pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.IsNull(testNode.ParentRepeatNode);
        }

        [Test]
        public void CorrectNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
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
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
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
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            Assert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test]
        public void CreatedActionRefTask()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            Assert.IsNotNull(testTask);
        }

        [Test]
        public void CreatedActionRefTask1()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;

            Assert.AreEqual(NodeName.actionRef, testTask.Node.Name);
        }

        [Test]
        public void CreatedActionRefTask2()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;

            Assert.AreEqual("circle", testTask.Node.Label);
        }

        [Test]
        public void CreatedActionTask()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            Assert.AreEqual(1, testTask.ChildTasks.Count);
        }

        [Test]
        public void CreatedActionTask1()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            Assert.IsNotNull(testTask.ChildTasks[0]);
        }

        [Test]
        public void CreatedActionTask2()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            Assert.IsNotNull(testActionTask);
        }

        [Test]
        public void CreatedActionTask3()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            Assert.IsNotNull(testActionTask.Node);
        }

        [Test]
        public void CreatedActionTask4()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            Assert.AreEqual(NodeName.action, testActionTask.Node.Name);
        }

        [Test]
        public void CreatedActionTask5()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            Assert.AreEqual("circle", testActionTask.Node.Label);
        }

        [Test]
        public void CreatedActionTask10()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.action) as ActionTask;
            Assert.IsNotNull(testTask);
        }

        [Test]
        public void CorrectNumberOfBullets()
        {
            var filename = TestUtils.GetFilePath(@"Content\AllRound.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            //there should be 11 bullets
            Assert.AreEqual(21, TestUtils.Manager.Movers.Count);
        }
    }
}