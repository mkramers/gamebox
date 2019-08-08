using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public class GridWidget : ViewWidgetBase
    {
        private readonly Vector2 m_cellSize;
        private readonly MultiDrawable<VertexArrayShape> m_drawable;

        protected GridWidget(IViewProvider _viewProvider, Vector2 _cellSize) : base(_viewProvider)
        {
            m_cellSize = _cellSize;
            m_drawable = new MultiDrawable<VertexArrayShape>();

            UpdateDrawable();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            UpdateDrawable();
        }

        private void UpdateDrawable()
        {
            IEnumerable<VertexArrayShape> gridDrawables = GetGridShapes(m_viewProvider.GetView());

            IEnumerable<VertexArrayShape> vertexArrayShapes =
                gridDrawables as VertexArrayShape[] ?? gridDrawables.ToArray();

            m_drawable.DisposeItemsAndClear();
            m_drawable.AddRange(vertexArrayShapes);
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable.Draw(_target, _states);
        }

        public override void Dispose()
        {
            m_drawable.Dispose();
        }

        private IEnumerable<VertexArrayShape> GetGridShapes(View _view)
        {
            View view = _view;
            Vector2f size = view.Size + new Vector2f(1, 1);

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), size);

            IEnumerable<VertexArrayShape> gridDrawables =
                GridDrawingUtilities.GetGridDrawableFromView(snappedView, m_cellSize);
            return gridDrawables;
        }
    }
}