using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridWidget : IRenderCoreWindowWidget
    {
        private readonly List<Shape> m_shapes;

        public GridWidget()
        {
            m_shapes = new List<Shape>();
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

        public void SetRenderPosition(Vector2 _position)
        {
            throw new NotImplementedException();
        }

        public void SetView(View _view)
        {
            ClearShapes();

            Vector2 snappedCenter = new Vector2((float)Math.Round(_view.Center.X), (float)Math.Round(_view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), _view.Size);

            IEnumerable<Shape> shapes = GridDrawingUtilities.GetGridDrawableFromView(snappedView);
            m_shapes.AddRange(shapes);
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