﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;

namespace MarchingSquares
{
    public static class MarchingSquaresVertexObjectGenerator
    {
        public static IEnumerable<IVertexObject> GenerateVertexObjects<T>(this MarchingSquaresGenerator<T> _marchingSquaresGenerator, IVertexObjectsGenerator _generator) where T : IComparable
        {
            IEnumerable<LineSegment> lineSegments = _marchingSquaresGenerator.GetLineSegments();

            IEnumerable<IVertexObject> polygons = _generator.GetVertexObjects(lineSegments);
            return polygons;
        }
    }

    public interface IVertexObjectsGenerator
    {
        IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments);
    }

    public class HeadToTailGenerator : IVertexObjectsGenerator
    {
        public IEnumerable<IVertexObject> GetVertexObjects(IEnumerable<LineSegment> _lineSegments)
        {
            List<Polygon> vertexObjects = new List<Polygon>();

            Polygon polygon = GetVertexObject(_lineSegments);
            vertexObjects.Add(polygon);

            return vertexObjects;
        }

        public Polygon GetVertexObject(IEnumerable<LineSegment> _lineSegments)
        {
            Polygon polygon = new Polygon();

            List<LineSegment> lineSegments = _lineSegments.ToList();

            //Console.WriteLine($"GetVertexObject called with {lineSegments.Count} LineSegments");

            LineSegment currentLine = lineSegments.First();
            lineSegments.Remove(currentLine);

            polygon.AddRange(currentLine);

            int count = 0;
            int max = lineSegments.Count;
            while (lineSegments.Any() && count < max)
            {
                //Console.WriteLine($"Looking for match of {currentLine.GetDisplayString()}\n\tin\n{lineSegments.Where(_lineSegment => _lineSegment != null).GetDisplayString()}");

                List<LineSegment> remainingLineSegments = new List<LineSegment>(lineSegments);

                foreach (LineSegment lineSegment in remainingLineSegments)
                {
                    bool isConnectedAtStart = currentLine.End.ApproximatelyEqualTo(lineSegment.Start);
                    bool isConnectedAtEnd = currentLine.End.ApproximatelyEqualTo(lineSegment.End);

                    if (!isConnectedAtStart && !isConnectedAtEnd)
                    {
                        continue;
                    }

                    LineSegment nextLine = lineSegment;
                    if (isConnectedAtEnd)
                    {
                        nextLine = lineSegment.GetFlipped();
                    }

                    //Console.WriteLine($"Found match!\nline = {nextLine.GetDisplayString()}\ncurrentLineEnd = {currentLine.GetDisplayString()}\ncount = {count}\n");

                    currentLine = nextLine;

                    polygon.Add(nextLine.End);
                    lineSegments.Remove(lineSegment);
                    break;
                }

                count++;
            }

            return polygon;
        }
    }
}