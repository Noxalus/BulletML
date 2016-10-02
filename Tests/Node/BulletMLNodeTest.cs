using BulletML;
using BulletML.Enums;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class BulletMLNodeTest
    {
        [Test]
        public void TestEmpty()
        {
            var filename = TestUtils.GetFilePath(@"Content\Empty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.AreEqual(filename, pattern.Filename);
            Assert.IsNull(pattern.RootNode.Label);
            Assert.AreEqual(PatternType.none, pattern.Orientation);

            Assert.IsNotNull(pattern.RootNode);
            Assert.AreEqual(0, pattern.RootNode.ChildNodes.Count);
            Assert.AreEqual(NodeName.bulletml, pattern.RootNode.Name);
            Assert.AreEqual(NodeType.unknown, pattern.RootNode.NodeType);
        }

        [Test]
        public void TestEmptyHoriz()
        {
            var filename = TestUtils.GetFilePath(@"Content\EmptyHoriz.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.AreEqual(filename, pattern.Filename);
            Assert.IsNull(pattern.RootNode.Label);
            Assert.AreEqual(PatternType.horizontal, pattern.Orientation);

            Assert.IsNotNull(pattern.RootNode);
            Assert.AreEqual(0, pattern.RootNode.ChildNodes.Count);
            Assert.AreEqual(NodeName.bulletml, pattern.RootNode.Name);
            Assert.AreEqual(NodeType.unknown, pattern.RootNode.NodeType);
        }

        [Test]
        public void TestEmptyVert()
        {
            var filename = TestUtils.GetFilePath(@"Content\EmptyVert.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.AreEqual(filename, pattern.Filename);
            Assert.IsNull(pattern.RootNode.Label);
            Assert.AreEqual(PatternType.vertical, pattern.Orientation);

            Assert.IsNotNull(pattern.RootNode);
            Assert.AreEqual(0, pattern.RootNode.ChildNodes.Count);
            Assert.AreEqual(NodeName.bulletml, pattern.RootNode.Name);
            Assert.AreEqual(NodeType.unknown, pattern.RootNode.NodeType);
        }

        [Test]
        public void TestIsParent()
        {
            var filename = TestUtils.GetFilePath(@"Content\Empty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.AreEqual(pattern.RootNode.GetRootNode(), pattern.RootNode);
        }
    }
}