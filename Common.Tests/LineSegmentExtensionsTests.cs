using System.Numerics;
using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class LineSegmentExtensionsTests
    {
        [Test]
        public void FlipsCorrectly()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);
            LineSegment lineSegment = new LineSegment(start, end);

            LineSegment flipped = lineSegment.GetFlipped();
            Assert.Multiple(() =>
            {
                Assert.That(flipped.Start, Is.EqualTo(end));
                Assert.That(flipped.End, Is.EqualTo(start));
            });
        }
    }
}