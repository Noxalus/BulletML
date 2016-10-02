using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture()]
    [Category("NodeTest")]
    public class ParamNodeTest
    {
        [Test]
        public void CreatedParamNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            Assert.IsNotNull(pattern.RootNode);
        }

        [Test]
        public void GotParamNode()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode.GetChild(NodeName.param));
        }

        [Test]
        public void GotParamNode1()
        {
            var filename = TestUtils.GetFilePath(@"Content\FireRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            Assert.IsNotNull(testFireNode.GetChild(NodeName.param) as ParamNode);
        }

        [Test]
        public void GotParamNode2()
        {
            var filename = TestUtils.GetFilePath(@"Content\BulletRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            Assert.IsNotNull(refNode.GetChild(NodeName.param) as ParamNode);
        }

        [Test]
        public void GotParamNode3()
        {
            var filename = TestUtils.GetFilePath(@"Content\ActionRefParam.xml");
            BulletPattern pattern = new BulletPattern();
            pattern.Parse(filename);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            Assert.IsNotNull(testActionRefNode.GetChild(NodeName.param) as ParamNode);
        }
    }
}