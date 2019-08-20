using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using LibExtensions;
using RenderCore.Drawable;
using SFML.Graphics;

namespace RenderCore.ShapeUtilities
{
    public static class ShapeFactory
    {
        public static IEnumerable<VertexArrayShape> GetGridShapes(int _rows, int _columns, Vector2 _size,
            Vector2 _position)
        {
            Vector2 cellSize = new Vector2(_size.X / _columns, _size.Y / _rows);

            List<LineSegment> segments = new List<LineSegment>(_rows + _columns);

            for (int i = 1; i < _rows; i++)
            {
                Vector2 start = new Vector2(0, i * cellSize.Y) + _position;
                Vector2 end = new Vector2(_size.X, i * cellSize.Y) + _position;

                segments.Add(new LineSegment(start, end));
            }

            for (int i = 1; i < _columns; i++)
            {
                Vector2 start = new Vector2(i * cellSize.X, 0) + _position;
                Vector2 end = new Vector2(i * cellSize.X, _size.Y) + _position;

                segments.Add(new LineSegment(start, end));
            }

            IEnumerable<VertexArrayShape> shapes = segments.Select(_segment =>
                VertexArrayShape.Factory.CreateLinesShape(_segment, Color.White));
            return shapes;
        }

        public static ConvexShape GetConvexShape(IVertexObject _vertices)
        {
            ConvexShape shape = new ConvexShape((uint) _vertices.Count);

            for (int i = 0; i < _vertices.Count; i++)
            {
                Vector2 vertex = _vertices[i];

                shape.SetPoint((uint) i, vertex.GetVector2F());
            }

            return shape;
        }

        public static Polygon CreateRectangle(Vector2 _halfSize)
        {
            Polygon polygon = new Polygon
            {
                new Vector2(-_halfSize.X, -_halfSize.Y),
                new Vector2(_halfSize.X, -_halfSize.Y),
                new Vector2(_halfSize.X, _halfSize.Y),
                new Vector2(-_halfSize.X, _halfSize.Y)
            };

            return polygon;
        }
    }
}