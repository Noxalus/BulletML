using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class ActionRefNodeTest
    {
        [Test]
        public void ValidXml()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void GotActionRefNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
        }

        [Test]
        public void GotActionRefNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
        }

        [Test]
        public void GotActionRefNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
        }

        [Test]
        public void GotActionRefNode3()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            Assert.IsNotNull(testBulletNode.GetChild(NodeName.actionRef));
        }

        [Test]
        public void GotActionRefNode4()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            Assert.IsNotNull(testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode);
        }

        [Test]
        public void FoundActionNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            var testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            Assert.IsNotNull(testActionRefNode);
            Assert.IsNotNull(testActionRefNode.ReferencedActionNode);
        }

        [Test]
        public void FoundActionNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            var testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            Assert.IsNotNull(testActionRefNode);
            Assert.IsNotNull(testActionRefNode.ReferencedActionNode);
        }

        [Test]
        public void FoundActionNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            var testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            Assert.IsNotNull(testActionRefNode);
            var refNode = testActionRefNode.ReferencedActionNode;
            Assert.IsNotNull(testFireNode);
            Assert.AreEqual("test", refNode.Label);
        }

        [Test]
        public void FoundCorrectActionNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            Assert.IsNotNull(testActionRefNode);
            ActionNode refNode = testActionRefNode.ReferencedActionNode;
            Assert.IsNotNull(testActionNode);
            Assert.AreEqual("test2", refNode.Label);
        }
    }
}