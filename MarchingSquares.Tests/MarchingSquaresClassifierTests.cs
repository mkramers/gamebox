using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;
using NUnit.Framework;

namespace MarchingSquares.Tests
{
    [TestFixture]
    public class MarchingSquaresClassifierTests
    {
        private static IEnumerable TestCases
        {
            get
            {
                TestCaseData GetTestCaseData(IReadOnlyList<bool> _grid, IReadOnlyList<byte> _classifiedGrid)
                {
                    Grid<bool> grid = GridTestUtilities.CreateSquareGridFromArray(_grid);

                    Grid<byte> classifiedGrid = GridTestUtilities.CreateSquareGridFromArray(_classifiedGrid);

                    return new TestCaseData(grid, classifiedGrid);
                }

                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, true, false,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 0, 0, 0, 0, 0, 0, 2, 3, 1, 0, 0, 6, 15, 9, 0, 0, 4, 12, 8, 0, 0, 0, 0, 0, 0 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, true, false, true,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 0, 0, 0, 0, 0, 0, 2, 3, 1, 0, 0, 6, 13, 10, 1, 0, 4, 8, 4, 8, 0, 0, 0, 0, 0 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        true, true, true, true,
                        true, false, false, true,
                        true, false, false, true,
                        true, true, true, true,
                    };
                    byte[] classifiedValues = { 2, 3, 3, 3, 1, 6, 13, 12, 14, 9, 6, 9, 0, 6, 9, 6, 11, 3, 7, 9, 4, 12, 12, 12, 8 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
                {
                    bool[] gridValues =
                    {
                        false, false, false, false,
                        false, true, true, false,
                        false, false, false, false,
                        false, false, false, false,
                    };
                    byte[] classifiedValues = { 0, 0, 0, 0, 0, 0, 2, 3, 1, 0, 0, 4, 12, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    yield return GetTestCaseData(gridValues, classifiedValues).Returns(true);
                }
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public bool ClassifiesCorrectly(Grid<bool> _binaryMask, Grid<byte> _expectedClassifiedGrid)
        {
            Grid<byte> classifiedGrid = MarchingSquaresClassifier.ClassifyCells(_binaryMask);

            Debug.WriteLine($"Classification result\t\t{string.Join(", ", classifiedGrid.Select(_cell => _cell.Value))}");
            Debug.WriteLine($"Classification expected\t\t{string.Join(", ", _expectedClassifiedGrid.Select(_cell => _cell.Value))}");

            bool isEqual = classifiedGrid.Select(_cell => _cell.Value).SequenceEqual(_expectedClassifiedGrid.Select(_cell => _cell.Value));

            return isEqual;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void GeneratesPolygonsCorrectly()
        {
            List<LineSegment> linesSegments = new List<LineSegment>
            {
                new LineSegment(new Vector2(0,0), new Vector2(1,0)),
                new LineSegment(new Vector2(1,0), new Vector2(1,1)),
                new LineSegment(new Vector2(1,1), new Vector2(0,1)),
                new LineSegment(new Vector2(0, 1), new Vector2(0,0))
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

            IVertexObject[] polygons = VertexObjectHeadTailBuilder.GetVertexObjects(linesSegments).ToArray();

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