using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridWidget : MultiDrawable<Shape>, IWidget
    {
        private readonly IViewProvider m_viewProvider;
        private readonly float m_lineThickness;
        private readonly Vector2 m_cellSize;

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

            IEnumerable<Shape> gridDrawables = GridDrawingUtilities.GetGridDrawableFromView(snappedView, m_lineThickness, m_cellSize);

            DisposeItemsAndClear();
            AddRange(gridDrawables);
        }
    }
}