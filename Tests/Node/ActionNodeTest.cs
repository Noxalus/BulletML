using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture]
    [Category("NodeTest")]
    public class ActionNodeTest
    {
        [Test]
        public void TestOneTop()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.IsNotNull(testNode);
        }

        [Test]
        public void TestNoRepeatNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionOneTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;

            Assert.IsNotNull(testNode);
            Assert.IsNull(testNode.ParentRepeatNode);
        }

        [Test]
        public void TestManyTop()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionManyTop.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testNode1 = pattern.RootNode.FindLabelNode("top1", NodeName.action) as ActionNode;
            var testNode2 = pattern.RootNode.FindLabelNode("top2", NodeName.action) as ActionNode;

            Assert.IsNotNull(testNode1);
            Assert.IsNotNull(testNode2);
        }
    }
}