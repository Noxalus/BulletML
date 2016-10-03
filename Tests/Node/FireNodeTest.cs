using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class FireNodeTest
    {
        [Test]
        public void CreatedFireNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
            Assert.AreEqual(1, pattern.RootNode.ChildNodes.Count);
        }

        [Test]
        public void CreatedFireNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            // Get the child action node
            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire));
        }

        [Test]
        public void CreatedFireNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire));

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);
            Assert.AreEqual("test", testFireNode.Label);
        }

        [Test]
        public void GotBulletNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireActionEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.BulletDescriptionNode);
        }

        [Test]
        public void CreatedTopLevelFireNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testFireNode = pattern.RootNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.BulletDescriptionNode);
            Assert.AreEqual("test", testFireNode.Label);
        }
    }
}