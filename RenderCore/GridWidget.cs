using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
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