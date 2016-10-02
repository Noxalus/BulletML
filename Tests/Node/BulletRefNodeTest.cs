using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class BulletRefNodeTest
    {
        [Test]
        public void ValidXml()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void SetBulletLabelNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;
            Assert.IsNotNull(testBulletNode);
            Assert.AreEqual("test", testBulletNode.Label);
        }

        [Test]
        public void CreatedBulletRefNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire));
            Assert.IsNotNull(testActionNode.GetChild(NodeName.fire) as FireNode);
        }

        [Test]
        public void CreatedBulletRefNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.GetChild(NodeName.bulletRef));
        }

        [Test]
        public void CreatedBulletRefNode3()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            Assert.IsNotNull(testFireNode.GetChild(NodeName.bulletRef));
            Assert.IsNotNull(testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode);
        }

        [Test]
        public void FoundBulletNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            Assert.IsNotNull(refNode);
            Assert.IsNotNull(refNode.ReferencedBulletNode);
        }

        [Test]
        public void FoundBulletNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            Assert.IsNotNull(refNode);
            Assert.IsNotNull(refNode.ReferencedBulletNode);
        }

        [Test]
        public void FoundBulletNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRef.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            Assert.IsNotNull(refNode);
            var testBulletNode = refNode.ReferencedBulletNode;

            Assert.AreEqual("test", testBulletNode.Label);
        }

        [Test]
        public void FoundCorrectBulletNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefTwoBullets.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            Assert.IsNotNull(testActionNode);
            var testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            Assert.IsNotNull(testFireNode);
            var refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            Assert.IsNotNull(refNode);
            var testBulletNode = refNode.ReferencedBulletNode;

            Assert.AreEqual("test2", testBulletNode.Label);
        }
    }
}