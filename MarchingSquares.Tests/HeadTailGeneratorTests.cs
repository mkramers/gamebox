﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.Geometry;
using Common.VertexObject;
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
                        new LineSegment(0,0,1,0),
                        new LineSegment(1,0,1,1),
                        new LineSegment(1,1,0,1),
                        new LineSegment(0,1,0,0),
                    };

                    Polygon polygon = new Polygon
                    {
                        new Vector2(0,0),
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                    };

                    yield return new TestCaseData(lines, polygon);
                }
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void GeneratesPolygonCorrectly(List<LineSegment> _lineSegments, Polygon _expectedPolygon)
        {
            HeadToTailGenerator generator = new HeadToTailGenerator();

            Polygon polygons = generator.GetVertexObject(_lineSegments);

            CollectionAssert.AreEqual(polygons, _expectedPolygon);
        }
    }
}