using Microsoft.Xna.Framework;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class DirectionTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectNumberOfBullets()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;
            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(1, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void CorrectNumberOfBullets1()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;
            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();
            Assert.AreEqual(2, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void CorrectNumberOfBullets2()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;
            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            //run the thing ten times
            for (int i = 2; i < 12; i++)
            {
                TestUtils.Manager.Update();
                Assert.AreEqual(i, TestUtils.Manager.Movers.Count);
            }

            //there should be 11 bullets
            Assert.AreEqual(11, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void CorrectDirection()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Run the engine ten times
            for (var i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            for (var i = 1; i < TestUtils.Manager.Movers.Count; i++)
            {
                var bullet = TestUtils.Manager.Movers[i];
                var direction = MathHelper.ToDegrees(bullet.Direction);

                Assert.AreEqual(90.0f, direction);
            }
        }

        [Test]
        public void SpeedInitializedCorrect()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Get the fire task
            var fireTask = mover.FindTaskByLabel("fireTask") as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.SpeedTask);
        }

        [Test]
        public void SpeedInitializedCorrect1()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            var fireTask = mover.FindTaskByLabel("fireTask") as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.IsNotNull(fireTask.SpeedTask);
        }

        [Test]
        public void SpeedInitializedCorrect2()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            var fireTask = mover.FindTaskByLabel("fireTask") as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(1.0f, fireTask.SpeedTask.GetNodeValue(mover));
        }

        [Test]
        public void SpeedInitializedCorrect3()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            BulletML.Tasks.FireTask fireTask = mover.FindTaskByLabel("fireTask") as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
        }

        [Test]
        public void SpeedInitializedCorrect4()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            var fireTask = mover.FindTaskByLabel("fireTask") as BulletML.Tasks.FireTask;

            Assert.IsNotNull(fireTask);
            Assert.AreEqual(1.0f, fireTask.FireSpeed);
        }

        [Test]
        public void CorrectSpeed()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Run the engine ten times
            for (var i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            // Check the top bullet
            var bullet = TestUtils.Manager.Movers[0];
            Assert.AreEqual(0, bullet.Speed);

            // Check the second bullet
            bullet = TestUtils.Manager.Movers[1];
            Assert.AreEqual(1, bullet.Speed);
        }

        [Test]
        public void CorrectSpeed1()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\Aim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // Run the engine ten times
            for (int i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            for (int i = 1; i < TestUtils.Manager.Movers.Count; i++)
            {
                var bullet = TestUtils.Manager.Movers[i];
                Assert.AreEqual(i, bullet.Speed);
            }
        }
    }
}