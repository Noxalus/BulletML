using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class SpeedNodeTest
    {
        [Test]
        public void CreatedSpeedNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void CreatedSpeedNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire));
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
        }

        [Test]
        public void CreatedSpeedNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.GetChild(NodeName.speed));
        }

        [Test]
        public void CreatedSpeedNode3()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.GetChild(NodeName.speed) as SpeedNode);
        }

        [Test]
        public void SpeedNodeDefaultValue()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeed.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;
            Assert.IsNotNull(testSpeedNode);

            Assert.AreEqual(NodeType.absolute, testSpeedNode.NodeType);
        }

        [Test]
        public void SpeedNodeAbsolute()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeedAbsolute.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;
            Assert.IsNotNull(testSpeedNode);

            Assert.AreEqual(NodeType.absolute, testSpeedNode.NodeType);
        }

        [Test]
        public void SpeedNodeSequence()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeedSequence.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;
            Assert.IsNotNull(testSpeedNode);

            Assert.AreEqual(NodeType.sequence, testSpeedNode.NodeType);
        }

        [Test]
        public void SpeedNodeRelative()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireSpeedRelative.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;
            Assert.IsNotNull(testSpeedNode);

            Assert.AreEqual(NodeType.relative, testSpeedNode.NodeType);
        }
    }
}