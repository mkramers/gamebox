using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly List<Drawable> m_renderables;

        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
            m_renderables = new List<Drawable>();
        }

        public void AddDrawable(Drawable _drawable)
        {
            Debug.Assert(_drawable != null);

            m_renderables.Add(_drawable);
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            foreach (Drawable drawable in m_renderables)
            {
                _renderWindow.Draw(drawable);
            }

            _renderWindow.Display();
        }
    }

    public class LineSegment
    {
        public Vector2 End { get; }

        public LineSegment(Vector2 _start, Vector2 _end)
        {
            Start = _start;
            End = _end;
        }

        public float Length => Vector.Length();
        public Vector2 Start { get; }
        public Vector2 Vector => End - Start;
        public Vector2 Direction => Vector2.Normalize(Vector);
    }

    public static class DrawableFactory
    {
        public static Drawable GetGrid(int _rows, int _columns, Vector2 _size, float _lineThickness, Vector2 _position)
        {
            Vector2 cellSize = new Vector2(_size.X / _columns, _size.Y / _rows);

            List<LineSegment> segments = new List<LineSegment>(_rows + _columns);

            for (int i = 1; i < _rows; i++)
            {
                Vector2 start = new Vector2(0, i * cellSize.Y - _lineThickness/2.0f) + _position;
                Vector2 end = new Vector2(_size.X, i * cellSize.Y - _lineThickness / 2.0f) + _position;

                segments.Add(new LineSegment(start, end));
            }
            for (int i = 1; i < _columns; i++)
            {
                Vector2 start = new Vector2(i * cellSize.X - _lineThickness / 2.0f, _size.Y) + _position;
                Vector2 end = new Vector2(i * cellSize.X - _lineThickness / 2.0f, 0) + _position;

                segments.Add(new LineSegment(start, end));
            }

            IEnumerable<Drawable> drawables = segments.Select(_segment => GetLine(_segment, _lineThickness));

            AssemblyDrawable assemblyDrawable = new AssemblyDrawable(drawables);
            return assemblyDrawable;
        }

        public static Drawable GetLine(LineSegment _line, float _thickness)
        {
            Vector2f size = new Vector2f(_thickness, _line.Length);
            float dotProduct = Vector2.Dot(_line.Direction, -Vector2.UnitY);
            float angle = ((float) Math.Acos(dotProduct)).ToDegrees();

            RectangleShape rectangleShape = new RectangleShape(size)
            {
                Position = _line.End.GetVector2f(),
                Rotation = angle,
            };
            return rectangleShape;
        }
    }

    public class AssemblyDrawable : Drawable
    {
        private readonly IEnumerable<Drawable> m_drawables;

        public AssemblyDrawable(IEnumerable<Drawable> _drawables)
        {
            m_drawables = _drawables;
        }

        public void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Drawable drawable in m_drawables)
            {
                _target.Draw(drawable, _state);
            }
        }
    }

    public static class ConvertToDegreesExtension
    {
        public static float ToDegrees(this float _radians)
        {
            return 180.0f * _radians / (float) Math.PI;
        }
    }
}