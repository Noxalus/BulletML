using Microsoft.Xna.Framework;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Task.FireTask
{
    [TestFixture()]
    [Category("TaskTest")]
    [Category("FireTaskTest")]
    public class FireTaskDirectionTest
    {
        [SetUp]
        public void SetupHarness()
        {
            TestUtils.Initialize();
        }

        [Test]
        public void IgnoreSequenceInitSpeed()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireDirectionSequence.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(10f, direction);
        }

        [Test]
        public void FireAbsDirection()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionAbsolute.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(10.0f, direction);
        }

        [Test]
        public void FireRelDirection()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionRelative.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.Direction = MathHelper.ToRadians(100.0f);
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            var direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(110f, direction);
        }

        [Test]
        public void FireAimDirection()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireDirectionAim.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(direction, 90.0f);
        }

        [Test]
        public void FireDefaultDirection()
        {
            TestUtils.Player.Position.X = 100.0f;
            TestUtils.Player.Position.Y = 0.0f;

            var filename = TestUtils.GetFilePath(@"Content\FireDirection.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(100.0f, direction, 0.0001f);
        }

        [Test]
        public void NestedBulletsDirection()
        {
            var filename = TestUtils.GetFilePath(@"Content\NestedBulletsDirection.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[2];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(20.0f, direction);
        }

        [Test]
        public void NestedBulletsDirection1()
        {
            var filename = TestUtils.GetFilePath(@"Content\NestedBulletsDirection.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[2];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(20.0f, direction);
        }

        [Test]
        public void InitDirectionWithSequence()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionBulletDirection.xml");
            TestUtils.Pattern.Parse(filename);

            var mover = (Mover)TestUtils.Manager.CreateBullet();
            mover.InitTopNode(TestUtils.Pattern.RootNode);

            TestUtils.Manager.Update();

            var bullet = TestUtils.Manager.Movers[1];
            float direction = MathHelper.ToDegrees(bullet.Direction);

            Assert.AreEqual(10.0f, direction);
        }
    }
}