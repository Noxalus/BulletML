using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class DirectionNodeTest
    {
        [Test]
        public void CreatedDirectionNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirection.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void CreatedDirectionNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirection.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire));

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);
        }

        [Test]
        public void CreatedDirectionNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirection.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.GetChild(NodeName.direction));
            Assert.IsNotNull(testFireNode.GetChild(NodeName.direction) as DirectionNode);
        }

        [Test]
        public void DirectionNodeDefaultValue()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirection.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);

            var testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(testDirectionNode);
            Assert.AreEqual(NodeType.aim, testDirectionNode.NodeType);
        }

        [Test]
        public void DirectionNodeAim()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionAim.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);

            var testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(testDirectionNode);
            Assert.AreEqual(NodeType.aim, testDirectionNode.NodeType);
        }

        [Test]
        public void DirectionNodeAbsolute()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionAbsolute.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);

            var testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(testDirectionNode);
            Assert.AreEqual(NodeType.absolute, testDirectionNode.NodeType);
        }

        [Test]
        public void DirectionNodeSequence()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionSequence.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);

            var testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(testDirectionNode);
            Assert.AreEqual(NodeType.sequence, testDirectionNode.NodeType);
        }

        [Test]
        public void DirectionNodeSequence2()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionSequence2.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var actionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(actionNode);
            Assert.AreEqual(2, actionNode.ChildNodes.Count);

            var fireNode1 = actionNode.ChildNodes[0] as FireNode;
            var fireNode2 = actionNode.ChildNodes[1] as FireNode;

            Assert.IsNotNull(fireNode1);
            Assert.IsNotNull(fireNode2);

            var directionNode1 = fireNode1.GetChild(NodeName.direction) as DirectionNode;
            var directionNode2 = fireNode2.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(directionNode1);
            Assert.IsNotNull(directionNode2);
            Assert.AreEqual(NodeType.absolute, directionNode1.NodeType);
            Assert.AreEqual(NodeType.sequence, directionNode2.NodeType);
        }

        [Test]
        public void DirectionNodeRelative()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireDirectionRelative.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;

            Assert.IsNotNull(testActionNode);

            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;

            Assert.IsNotNull(testFireNode);

            var testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            Assert.IsNotNull(testDirectionNode);
            Assert.AreEqual(NodeType.relative, testDirectionNode.NodeType);
        }
    }
}