using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class BulletNodeTest
    {
        [Test]
        public void CreatedBulletNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\EmptyBullet.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
            Assert.AreEqual(1, pattern.RootNode.ChildNodes.Count);
        }

        [Test]
        public void CreatedBulletNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\EmptyBullet.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;

            Assert.IsNotNull(testBulletNode);
        }

        [Test]
        public void SetBulletLabelNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\EmptyBullet.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;

            Assert.IsNotNull(testBulletNode);
            Assert.AreEqual("test", testBulletNode.Label);
        }
    }
}