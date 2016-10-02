using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class FireRefNodeTest
    {
        [Test]
        public void CreatedFireRefNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void CreatedFireNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            // Get the child action node
            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fireRef));
        }

        [Test]
        public void CreatedFireNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fireRef));
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fireRef) as FireRefNode);
        }

        [Test]
        public void GotFireNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.ReferencedFireNode);
        }

        [Test]
        public void GotFireNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.ReferencedFireNode as FireNode);
        }

        [Test]
        public void GotCorrectFireNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode);
            var fireNode = testFireNode.ReferencedFireNode as FireNode;
            Assert.AreEqual("test", fireNode.Label);
        }

        [Test]
        public void NoBulletNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNull(testFireNode.BulletDescriptionNode);
        }
    }
}