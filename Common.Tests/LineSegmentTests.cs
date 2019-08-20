using System.Numerics;
using Common.Geometry;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class LineSegmentTests
    {
        [Test]
        public void EndIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 1);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(end, Is.EqualTo(lineSegment.End));
        }

        [Test]
        public void StartIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 1);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(start, Is.EqualTo(lineSegment.Start));
        }

        [Test]
        public void LengthIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);

            float length = (end - start).Length();

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(length, Is.EqualTo(lineSegment.Length));
        }

        [Test]
        public void DirectionIsCorrect()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(1, 0);

            Vector2 direction = Vector2.Normalize(end - start);

            LineSegment lineSegment = new LineSegment(start, end);

            Assert.That(direction, Is.EqualTo(lineSegment.Direction));
        }
    }
}