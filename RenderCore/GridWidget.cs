using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridWidget : RenderCoreWidgetBase
    {
        private readonly IViewProvider m_viewProvider;
        private readonly List<Shape> m_shapes;

        public GridWidget(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
            m_shapes = new List<Shape>();
        }

        public override void Draw(RenderTarget _target, RenderStates _state)
        {
            foreach (Shape shape in m_shapes)
            {
                _target.Draw(shape, _state);
            }
        }

        public override void Dispose()
        {
            ClearShapes();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            ClearShapes();

            View view = m_viewProvider.GetView();

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), view.Size);

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