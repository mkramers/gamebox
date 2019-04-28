using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SFML;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public static class DrawableFactory
    {
        public static ShapeAssemblyDrawable GetGrid(int _rows, int _columns, Vector2 _size, float _lineThickness, Vector2 _position)
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
                Vector2 start = new Vector2(i * cellSize.X, _size.Y) + _position;
                Vector2 end = new Vector2(i * cellSize.X, 0) + _position;

                segments.Add(new LineSegment(start, end));
            }

            IEnumerable<Shape> drawables = segments.Select(_segment => GetLine(_segment, _lineThickness));

            ShapeAssemblyDrawable shapeAssemblyDrawable = new ShapeAssemblyDrawable(drawables);
            return shapeAssemblyDrawable;
        }

        private static RectangleShape GetLine(LineSegment _line, float _thickness)
        {
            Vector2f size = new Vector2f(_thickness, _line.Length);
            float dotProduct = Vector2.Dot(_line.Direction, -Vector2.UnitY);
            float angle = ((float) Math.Acos(dotProduct)).ToDegrees();

            RectangleShape rectangleShape = new RectangleShape(size)
            {
                Position = _line.End.GetVector2f(),
                Rotation = angle
            };
            return rectangleShape;
        }
    }
}