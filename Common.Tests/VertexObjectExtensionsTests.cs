using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.VertexObject;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class VertexObjectExtensionsTests
    {
        [Test]
        public void TranslatesCorrectly()
        {
            Vector2 translation = new Vector2(1, 1);

            Vector2 vector = new Vector2(0, 0);
            Polygon polygon = new Polygon { vector };

            IVertexObject translated = polygon.Translate(translation);
            Assert.That(translated.First(), Is.EqualTo(translation));
        }
    }
}