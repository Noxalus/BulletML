using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class AccelTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectSpeedAbs()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelAbs.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            Assert.AreEqual(20.0f, mover.Acceleration.X);
            Assert.AreEqual(40.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedAbs1()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelAbs.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(19.0f, mover.Acceleration.X);
            Assert.AreEqual(38.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedAbs2()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelAbs.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                TestUtils.Manager.Update();
            }

            Assert.AreEqual(10.0f, mover.Acceleration.X);
            Assert.AreEqual(20.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedRel()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(21.0f, mover.Acceleration.X);
            Assert.AreEqual(42.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedRel1()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                TestUtils.Manager.Update();
            }

            Assert.AreEqual(30.0f, mover.Acceleration.X);
            Assert.AreEqual(60.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedRel2()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            BulletMLTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel);
            Assert.IsNotNull(myTask);
        }

        [Test]
        public void CorrectSpeedRel3()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            Assert.IsNotNull(myTask);
        }

        [Test]
        public void CorrectSpeedRel4()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            Assert.AreEqual(1.0f, myTask.Acceleration.X);
            Assert.AreEqual(2.0f, myTask.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedRel5()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.horizontal);
            Assert.AreEqual(10.0f, myNode.GetValue(myTask));
        }

        [Test]
        public void CorrectSpeedRel6()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.vertical);
            Assert.AreEqual(NodeType.relative, myNode.NodeType);
        }

        [Test]
        public void CorrectSpeedRel7()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.horizontal);
            Assert.AreEqual(NodeType.relative, myNode.NodeType);
        }

        [Test]
        public void CorrectSpeedRel8()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.vertical);
            Assert.AreEqual(20.0f, myNode.GetValue(myTask));
        }

        [Test]
        public void CorrectSpeedRel9()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelRel.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            Assert.AreEqual(10.0f, myTask.Duration);
        }

        [Test]
        public void CorrectSpeedSeq()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelSeq.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            Assert.AreEqual(1.0f, myTask.Acceleration.X);
            Assert.AreEqual(2.0f, myTask.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedSeq1()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelSeq.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(21.0f, mover.Acceleration.X);
            Assert.AreEqual(42.0f, mover.Acceleration.Y);
        }

        [Test]
        public void CorrectSpeedSeq2()
        {
            var filename = TestUtils.GetFilePath(@"Content\AccelSeq.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                TestUtils.Manager.Update();
            }

            Assert.AreEqual(30.0f, mover.Acceleration.X);
            Assert.AreEqual(60.0f, mover.Acceleration.Y);
        }
    }
}