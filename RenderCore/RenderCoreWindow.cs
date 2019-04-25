using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Vector2 m_start;
        private readonly Vector2 m_end;

        public LineSegment(Vector2 _start, Vector2 _end)
        {
            m_start = _start;
            m_end = _end;
        }

        public float Length => (m_start - m_end).Length();
    }

    public class LineDrawable : Drawable
    {
        private readonly LineSegment m_line;
        private readonly float m_thickness;

        public LineDrawable(LineSegment _line, float _thickness)
        {
            m_line = _line;
            m_thickness = _thickness;
        }

        public void Draw(RenderTarget _renderTarget, RenderStates _state)
        {
            Vector2f size = new Vector2f(m_thickness, m_line.Length);

            RectangleShape rectangleShape = new RectangleShape(size);
            rectangleShape.Draw(_renderTarget, _state);
        }
    }

    public class GridDrawableFactory
    {
        public static AssemblyDrawable GetGrid(int _rows, int _columns, Vector2 _size, float _lineThickness)
        {
            Vector2 cellSize = new Vector2(_size.X / _columns, _size.Y / _rows);

            List<Drawable> renderables = new List<Drawable>();

            for (int i = 0; i < _rows; i++)
            {
                Vector2 start = new Vector2();
                Vector2 end = new Vector2();
                LineSegment lineSegment = new LineSegment(start, end);
                LineDrawable line = new LineDrawable(lineSegment, _lineThickness);

                renderables.Add(line);
            }

            AssemblyDrawable assemblyDrawable = new AssemblyDrawable(renderables);
            return assemblyDrawable;
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
}