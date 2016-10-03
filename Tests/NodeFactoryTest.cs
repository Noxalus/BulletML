using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;

namespace Tests
{
    [TestFixture()]
    public class NodeFactoryTest
    {
        [Test]
        public void BulletTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.bullet);
            Assert.IsTrue(testNode is BulletNode);
            Assert.AreEqual(NodeName.bullet, testNode.Name);
        }

        [Test]
        public void ActionTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.action);
            Assert.IsTrue(testNode is ActionNode);
            Assert.AreEqual(NodeName.action, testNode.Name);
        }

        [Test]
        public void FireTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.fire);
            Assert.IsTrue(testNode is FireNode);
            Assert.AreEqual(NodeName.fire, testNode.Name);
        }

        [Test]
        public void ChangeDirectionTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.changeDirection);
            Assert.IsTrue(testNode is ChangeDirectionNode);
            Assert.AreEqual(NodeName.changeDirection, testNode.Name);
        }

        [Test]
        public void ChangeSpeedTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.changeSpeed);
            Assert.IsTrue(testNode is ChangeSpeedNode);
            Assert.AreEqual(NodeName.changeSpeed, testNode.Name);
        }

        [Test]
        public void AccelTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.accel);
            Assert.IsTrue(testNode is AccelNode);
            Assert.AreEqual(NodeName.accel, testNode.Name);
        }

        [Test]
        public void WaitTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.wait);
            Assert.IsTrue(testNode is WaitNode);
            Assert.AreEqual(NodeName.wait, testNode.Name);
        }

        [Test]
        public void RepeatTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.repeat);
            Assert.IsTrue(testNode is RepeatNode);
            Assert.AreEqual(NodeName.repeat, testNode.Name);
        }

        [Test]
        public void BulletRefTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.bulletRef);
            Assert.IsTrue(testNode is BulletRefNode);
            Assert.AreEqual(NodeName.bulletRef, testNode.Name);
        }

        [Test]
        public void ActionRefTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.actionRef);
            Assert.IsTrue(testNode is ActionRefNode);
            Assert.AreEqual(NodeName.actionRef, testNode.Name);
        }

        [Test]
        public void FireRefTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.fireRef);
            Assert.IsTrue(testNode is FireRefNode);
            Assert.AreEqual(NodeName.fireRef, testNode.Name);
        }

        [Test]
        public void VanishTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.vanish);
            Assert.IsTrue(testNode is VanishNode);
            Assert.AreEqual(NodeName.vanish, testNode.Name);
        }

        [Test]
        public void HorizontalTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.horizontal);
            Assert.IsTrue(testNode is HorizontalNode);
            Assert.AreEqual(NodeName.horizontal, testNode.Name);
        }

        [Test]
        public void VerticalTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.vertical);
            Assert.IsTrue(testNode is VerticalNode);
            Assert.AreEqual(NodeName.vertical, testNode.Name);
        }

        [Test]
        public void TermTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.term);
            Assert.IsTrue(testNode is TermNode);
            Assert.AreEqual(NodeName.term, testNode.Name);
        }

        [Test]
        public void TimesTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.times);
            Assert.IsTrue(testNode is TimesNode);
            Assert.AreEqual(NodeName.times, testNode.Name);
        }

        [Test]
        public void DirectionTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.direction);
            Assert.IsTrue(testNode is DirectionNode);
            Assert.AreEqual(NodeName.direction, testNode.Name);
        }

        [Test]
        public void SpeedTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.speed);
            Assert.IsTrue(testNode is SpeedNode);
            Assert.AreEqual(NodeName.speed, testNode.Name);
        }

        [Test]
        public void ParamTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.param);
            Assert.IsTrue(testNode is ParamNode);
            Assert.AreEqual(NodeName.param, testNode.Name);
        }

        [Test]
        public void BulletMlTestCase()
        {
            var testNode = NodeFactory.CreateNode(NodeName.bulletml);
            Assert.IsNotNull(testNode); // CreateNode() returns always a BulletMLNode
            Assert.AreEqual(NodeName.bulletml, testNode.Name);
        }
    }
}