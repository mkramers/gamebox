using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class ShapeFactory
    {
        public static IEnumerable<Shape> GetGridShapes(int _rows, int _columns, Vector2 _size, float _lineThickness,
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

            IEnumerable<Shape> shapes = segments.Select(_segment => GetLineShape(_segment, _lineThickness));
            return shapes;
        }

        public static RectangleShape GetLineShape(LineSegment _line, float _thickness)
        {
            Vector2f size = new Vector2f(_thickness, _line.Length);
            float dotProduct = Vector2.Dot(_line.Direction, Vector2.UnitY);
            float angle = -((float)Math.Acos(dotProduct)).ToDegrees();

            RectangleShape rectangleShape = new RectangleShape(size)
            {
                Origin = new Vector2f(_thickness / 2.0f, 0.0f),
                Position = _line.Start.GetVector2F(),
                Rotation = angle
            };
            return rectangleShape;
        }

        public static ConvexShape GetConvexShape(IVertexObject _vertices)
        {
            ConvexShape shape = new ConvexShape((uint)_vertices.Count);

            for (int i = 0; i < _vertices.Count; i++)
            {
                Vector2 vertex = _vertices[i];

                shape.SetPoint((uint)i, vertex.GetVector2F());
            }

            return shape;
        }

        public static Polygon CreateRectangle(Vector2 _position, Vector2 _halfSize)
        {
            Polygon rectangle = CreateRectangle(_halfSize);
            rectangle.Translate(_position);
            return rectangle;
        }

        private static Polygon CreateRectangle(Vector2 _halfSize)
        {
            Polygon polygon = new Polygon(4)
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