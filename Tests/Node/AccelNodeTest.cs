using BulletML;
using BulletML.Enums;
using BulletML.Nodes;
using NUnit.Framework;
using System.IO;
using Tests.Utils;

namespace Tests.Node
{
    [TestFixture]
    [Category("NodeTest")]
    public class AccelNodeTest
    {
        #region Invalid

        [Test]
        public void AccelNodeEmpty()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeEmpty.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        [Test]
        public void AccelNodeHorizontalNoTerm()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeHorizontalNoTerm.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        [Test]
        public void AccelNodeVerticalNoTerm()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeVerticalNoTerm.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        [Test]
        public void AccelNodeHorizontalVerticalNoTerm()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeHorizontalVerticalNoTerm.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        [Test]
        public void AccelNodeVerticalHorizontalNoTerm()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeVerticalHorizontalNoTerm.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        [Test]
        public void AccelNodeTermOnly()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Invalid\AccelNodeTermOnly.xml");
            var pattern = new BulletPattern();

            Assert.Throws<InvalidDataException>(() => pattern.Parse(filename));
        }

        #endregion Invalid

        #region Valid

        [Test]
        public void AccelNodeHorizontal()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Valid\AccelNodeHorizontal.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var topNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;

            Assert.IsNotNull(topNode);
            Assert.AreEqual(1, topNode.ChildNodes.Count);

            var accelNode = topNode.ChildNodes[0] as AccelNode;

            Assert.IsNotNull(accelNode);
            Assert.AreEqual(2, accelNode.ChildNodes.Count);

            var horizontalNode = accelNode.ChildNodes[0] as HorizontalNode;
            var termNode = accelNode.ChildNodes[1] as TermNode;

            Assert.IsNotNull(horizontalNode);
            Assert.IsNotNull(termNode);
        }

        [Test]
        public void AccelNodeVertical()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Valid\AccelNodeVertical.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var topNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;

            Assert.IsNotNull(topNode);
            Assert.AreEqual(1, topNode.ChildNodes.Count);

            var accelNode = topNode.ChildNodes[0] as AccelNode;

            Assert.IsNotNull(accelNode);
            Assert.AreEqual(2, accelNode.ChildNodes.Count);

            var verticalNode = accelNode.ChildNodes[0] as VerticalNode;
            var termNode = accelNode.ChildNodes[1] as TermNode;

            Assert.IsNotNull(verticalNode);
            Assert.IsNotNull(termNode);
        }

        [Test]
        public void AccelNodeHorizontalVertical()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Valid\AccelNodeHorizontalVertical.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var topNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;

            Assert.IsNotNull(topNode);
            Assert.AreEqual(1, topNode.ChildNodes.Count);

            var accelNode = topNode.ChildNodes[0] as AccelNode;

            Assert.IsNotNull(accelNode);
            Assert.AreEqual(3, accelNode.ChildNodes.Count);

            var horizontalNode = accelNode.ChildNodes[0] as HorizontalNode;
            var verticalNode = accelNode.ChildNodes[1] as VerticalNode;
            var termNode = accelNode.ChildNodes[2] as TermNode;

            Assert.IsNotNull(horizontalNode);
            Assert.IsNotNull(verticalNode);
            Assert.IsNotNull(termNode);
        }

        [Test]
        public void AccelNodeVerticalHorizontal()
        {
            var filename = TestUtils.GetFilePath(@"Content\Test\Accel\Valid\AccelNodeVerticalHorizontal.xml");
            var pattern = new BulletPattern();
            pattern.Parse(filename);

            var topNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;

            Assert.IsNotNull(topNode);
            Assert.AreEqual(1, topNode.ChildNodes.Count);

            var accelNode = topNode.ChildNodes[0] as AccelNode;

            Assert.IsNotNull(accelNode);
            Assert.AreEqual(3, accelNode.ChildNodes.Count);

            var verticalNode = accelNode.ChildNodes[0] as VerticalNode;
            var horizontalNode = accelNode.ChildNodes[1] as HorizontalNode;
            var termNode = accelNode.ChildNodes[2] as TermNode;

            Assert.IsNotNull(verticalNode);
            Assert.IsNotNull(horizontalNode);
            Assert.IsNotNull(termNode);
        }

        #endregion Valid
    }
}