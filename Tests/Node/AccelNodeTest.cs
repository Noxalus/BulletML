using System;
using System.IO;
using System.Xml.Schema;
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
    }
}