using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.VertexObject;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GeneratesPolygonsCorrectly()
        {
            List<LineSegment> linesSegments = new List<LineSegment>
            {
                new LineSegment(new Vector2(0, 0), new Vector2(1, 0)),
                new LineSegment(new Vector2(1, 0), new Vector2(1, 1)),
                new LineSegment(new Vector2(1, 1), new Vector2(0, 1)),
                new LineSegment(new Vector2(0, 1), new Vector2(0, 0))
            };

            IList<Polygon> expectedPolygons = new List<Polygon>
            {
                new Polygon(4)
                {
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 1),
                    new Vector2(0, 1)
                }
            };

            IVertexObjectsGenerator generator = new HeadToTailGenerator();
            IVertexObject[] polygons = generator.GetVertexObjects(linesSegments).ToArray();

            bool polygonsMatch = polygons.Length == expectedPolygons.Count;

            for (int i = 0; i < polygons.Length; i++)
            {
                IVertexObject actualVertexObject = polygons[i];
                IVertexObject expectedVertexObject = expectedPolygons[i];

                for (int j = 0; j < actualVertexObject.Count; j++)
                {
                    Vector2 actualVertex = actualVertexObject[j];
                    Vector2 expectedVertex = expectedVertexObject[j];

                    if (!actualVertex.Equals(expectedVertex))
                    {
                        polygonsMatch = false;
                        break;
                    }
                }

                if (!polygonsMatch)
                {
                    break;
                }
            }

            Assert.That(polygonsMatch, Is.True);
        }
    }
}