using System.Linq;
using System.Numerics;
using Common.Extensions;
using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests.Extensions
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