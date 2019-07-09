using System;
using System.Collections.Generic;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using RenderCore.ViewProvider;
using SFML.Graphics;

namespace RenderCore.Widget
{
    public class GridWidget : MultiDrawable<LineShape>, IWidget
    {
        private readonly Vector2 m_cellSize;
        private readonly float m_lineThickness;
        private readonly IViewProvider m_viewProvider;

        public GridWidget(IViewProvider _viewProvider, float _lineThickness, Vector2 _cellSize)
        {
            m_viewProvider = _viewProvider;
            m_lineThickness = _lineThickness;
            m_cellSize = _cellSize;
        }

        public void Tick(TimeSpan _elapsed)
        {
            View view = m_viewProvider.GetView();

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), view.Size);

            IEnumerable<LineShape> gridDrawables =
                GridDrawingUtilities.GetGridDrawableFromView(snappedView, m_lineThickness, m_cellSize);

            DisposeItemsAndClear();
            AddRange(gridDrawables);
        }
    }
}