using System.Collections.Generic;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowWidget : IRenderCoreWindowWidget
    {
        public bool IsDrawEnabled { get; set; }

        public abstract void Draw(RenderTarget _target, RenderStates _states);

        public abstract void Dispose();
    }

    public class GridWidget : RenderCoreWindowWidget
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

        public override void Draw(RenderTarget _target, RenderStates _state)
        {
            if (!IsDrawEnabled)
            {
                return;
            }

            foreach (Shape shape in m_shapes)
            {
                _target.Draw(shape, _state);
            }
        }

        public override void Dispose()
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