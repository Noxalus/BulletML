using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture]
    [Category("NodeTest")]
    public class AccelNodeTest
    {
        [Test]
        public void AccelNodeEmpty()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\AccelNodeEmpty.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            Assert.IsNotNull(testNode);
        }
    }
}