using Microsoft.Xna.Framework;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task
{
    [TestFixture()]
    [Category("TaskTest")]
    public class RepeatTaskTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void CorrectSpeed()
        {
            var filename = TestUtils.GetFilePath(@"Content\RepeatSequence.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            Assert.AreEqual(0, mover.Speed);
        }

        [Test]
        public void CorrectSpeed1()
        {
            var filename = TestUtils.GetFilePath(@"Content\RepeatSequence.xml");
            TestUtils.Pattern.Parse(filename);
            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            for (var i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            Assert.AreEqual(10, mover.Speed);
        }

        [Test]
        public void CorrectSpeed2()
        {
            var filename = TestUtils.GetFilePath(@"Content\RepeatSequence.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(1, mover.Speed);
        }

        [Test]
        public void RepeatDirectionSequence()
        {
            var filename = TestUtils.GetFilePath(@"Content\RepeatFireDirectionSequence.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            for (var i = 0; i < 10; i++)
                TestUtils.Manager.Update();

            for (var i = 0; i < 10; i++)
            {
                var bullet = TestUtils.Manager.Movers[i];
                var dir = MathHelper.ToDegrees(bullet.Direction);
                Assert.AreEqual(10 * i, dir);
            }
        }

        [Test]
        public void RepeatDirectionSequence2()
        {
            var filename = TestUtils.GetFilePath(@"Content\RepeatFireDirectionSequence2.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            // 2 repeat loop + the root node
            float expectedBulletNumber = (3 * 3) + 1;

            for (var i = 0; i < expectedBulletNumber; i++)
                TestUtils.Manager.Update();

            Assert.AreEqual(expectedBulletNumber, TestUtils.Manager.Movers.Count);

            for (var i = 1; i < expectedBulletNumber - 1; i++)
            {
                var bullet = TestUtils.Manager.Movers[i];
                var dir = MathHelper.ToDegrees(bullet.Direction);

                Assert.AreEqual((118 * i), dir, 10e-3);
            }
        }

        [Test]
        public void CorrectNumberOfBullets()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            //there should be 20 bullets
            Assert.AreEqual(20, TestUtils.Manager.Movers.Count);
        }

        [Test]
        public void CorrectSpeedFirstSet()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            Assert.AreEqual(TestUtils.Manager.Movers[0].Speed, 1);
        }

        [Test]
        public void CorrectSpeedFirstSet1()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            Assert.AreEqual(TestUtils.Manager.Movers[1].Speed, 2);
        }

        [Test]
        public void CorrectSpeedFirstSet2()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            Assert.AreEqual(TestUtils.Manager.Movers[0].Speed, 1);
            Assert.AreEqual(TestUtils.Manager.Movers[1].Speed, 2);
            Assert.AreEqual(TestUtils.Manager.Movers[2].Speed, 3);
            Assert.AreEqual(TestUtils.Manager.Movers[3].Speed, 4);
            Assert.AreEqual(TestUtils.Manager.Movers[4].Speed, 5);
            Assert.AreEqual(TestUtils.Manager.Movers[5].Speed, 6);
            Assert.AreEqual(TestUtils.Manager.Movers[6].Speed, 7);
            Assert.AreEqual(TestUtils.Manager.Movers[7].Speed, 8);
            Assert.AreEqual(TestUtils.Manager.Movers[8].Speed, 9);
            Assert.AreEqual(TestUtils.Manager.Movers[9].Speed, 10);
        }

        [Test]
        public void CorrectSpeedFirstSet3()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(i + 1, TestUtils.Manager.Movers[i].Speed);
            }
        }

        [Test]
        public void CorrectSpeedSecondSet()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            Assert.AreEqual(11f, TestUtils.Manager.Movers[10].Speed);
        }

        [Test]
        public void CorrectSpeedSecondSet1()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);
            Mover mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);
            TestUtils.Manager.Update();

            Assert.AreEqual(12f, TestUtils.Manager.Movers[11].Speed);
        }

        [Test]
        public void CorrectSpeedAll()
        {
            var filename = TestUtils.GetFilePath(@"Content\DoubleRepeat.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            for (int i = 0; i < 20; i++)
            {
                Assert.AreEqual(i + 1, TestUtils.Manager.Movers[i].Speed);
            }
        }
    }
}