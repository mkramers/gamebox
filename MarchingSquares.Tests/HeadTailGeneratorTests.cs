using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Common.Geometry;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class HeadTailGeneratorTests
    {
        private static IEnumerable TestCases
        {
            get
            {
                {
                    List<LineSegment> lines = new List<LineSegment>
                    {
                        new LineSegment(0, 0, 1, 0),
                        new LineSegment(1, 0, 1, 1),
                        new LineSegment(1, 1, 0, 1),
                        new LineSegment(0, 1, 0, 0)
                    };

                    Polygon polygon = new Polygon
                    {
                        new Vector2(0, 0),
                        new Vector2(1, 0),
                        new Vector2(1, 1),
                        new Vector2(0, 1),
                        new Vector2(0, 0)
                    };

                    yield return new TestCaseData(lines, polygon);
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void GeneratesPolygonCorrectly(List<LineSegment> _lineSegments, Polygon _expectedPolygon)
        {
            Polygon polygons = HeadToTailGenerator.GetVertexObject(_lineSegments);

            CollectionAssert.AreEqual(polygons, _expectedPolygon);
        }
    }
}