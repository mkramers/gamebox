using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridWidget : RenderCoreViewWidgetBase
    {
        private readonly List<Shape> m_shapes;

        public GridWidget()
        {
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

        public override void SetRenderPosition(Vector2 _positionScreen)
        {
            throw new NotImplementedException();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            ClearShapes();

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(m_view.Center.X), (float) Math.Round(m_view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), m_view.Size);

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