using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public static class BlockingCollectionExtensions
    {
        public static void Clear<T>(this BlockingCollection<T> _blockingCollection)
        {
            if (_blockingCollection == null)
            {
                throw new ArgumentNullException("_blockingCollection");
            }

            while (_blockingCollection.Count > 0)
            {
                T item;
                _blockingCollection.TryTake(out item);
            }
        }
    }

    public class GridDrawingUtilities
    {
        public static IEnumerable<Shape> GetGridDrawableFromView(View _view)
        {
            Vector2 viewSize = _view.Size.GetVector2();
            Vector2 position = _view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.X);
            int columns = (int) Math.Round(viewSize.Y);

            IEnumerable<Shape> shapes = DrawableFactory.GetGridShapes(rows, columns, viewSize, 0.05f, position);
            return shapes;
        }
    }

    public class RenderCoreWindow : RenderCoreWindowBase, IDisposable
    {
        private readonly List<Drawable> m_drawables;
        private readonly IEnumerable<IRenderCoreWindowWidget> m_widgets;
        private readonly object m_drawLock;

        public RenderCoreWindow(RenderWindow _renderWindow, IEnumerable<IRenderCoreWindowWidget> _widgets) : base(_renderWindow)
        {
            m_drawables = new List<Drawable>();
            m_widgets = _widgets;
            m_drawLock = new object();
        }

        public void Add(Drawable _drawable)
        {
            Debug.Assert(_drawable != null);

            lock (m_drawLock)
            {
                m_drawables.Add(_drawable);
            }
        }
        public void Add(IEnumerable<Drawable> _drawables)
        {
            Debug.Assert(_drawables != null);

            foreach (Drawable drawable in _drawables)
            {
                Add(drawable);
            }
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            lock (m_drawLock)
            {
                _renderWindow.Clear(Color.Green);

                foreach (IRenderCoreWindowWidget widget in m_widgets)
                {
                    _renderWindow.Draw(widget);
                }

                foreach (Drawable shape in m_drawables)
                {
                    _renderWindow.Draw(shape);
                }
                
                _renderWindow.Display();
            }
        }

        public void Dispose()
        {
            foreach (IRenderCoreWindowWidget widget in m_widgets)
            {
                widget.Dispose();
            }

            m_renderWindow.Dispose();
        }
    }

    public interface IRenderCoreWindowWidget: Drawable, IDisposable
    {

    }

    public class GridWidget : IRenderCoreWindowWidget
    {
        private readonly List<Shape> m_shapes;

        public GridWidget(View _view)
        {
            m_shapes = new List<Shape>();

            UpdateView(_view);
        }

        private void UpdateView(View _view)
        {
            ClearShapes();

            IEnumerable<Shape> shapes = GridDrawingUtilities.GetGridDrawableFromView(_view);
            m_shapes.AddRange(shapes);
        }

        public void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Shape shape in m_shapes)
            {
                _target.Draw(shape, _state);
            }
        }

        public void Dispose()
        {
            ClearShapes();
        }

        private void ClearShapes()
        {
            foreach (Shape shape in m_shapes)
            {
                shape.Dispose();
            }

            m_shapes.Clear();
        }
    }
}