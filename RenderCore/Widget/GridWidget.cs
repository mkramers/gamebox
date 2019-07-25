using System;
using System.Collections.Generic;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public class LabeledGridWidget : GridWidget
    {
        //private List<TickableDrawable<Text>> m_labels;

        public LabeledGridWidget(IViewProvider _viewProvider, float _lineThickness, Vector2 _cellSize) : base(_viewProvider, _lineThickness, _cellSize)
        {
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            View view = m_viewProvider.GetView();
            Vector2f size = view.Size + new Vector2f(2, 2);

            Vector2f topLeft = view.Center - size / 2;
        }
    }

    public abstract class ViewWidgetBase : MultiDrawable<VertexArrayShape>, IWidget
    {
        protected readonly IViewProvider m_viewProvider;

        protected ViewWidgetBase(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public abstract void Tick(TimeSpan _elapsed);
    }

    public class GridWidget : ViewWidgetBase
    {
        private readonly Vector2 m_cellSize;
        private readonly float m_lineThickness;

        public GridWidget(IViewProvider _viewProvider, float _lineThickness, Vector2 _cellSize) : base(_viewProvider)
        {
            m_lineThickness = _lineThickness;
            m_cellSize = _cellSize;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            IEnumerable<VertexArrayShape> gridDrawables = GetGridShapes(m_viewProvider.GetView());

            DisposeItemsAndClear();
            AddRange(gridDrawables);
        }

        protected IEnumerable<VertexArrayShape> GetGridShapes(View _view)
        {
            View view = _view;
            Vector2f size = view.Size + new Vector2f(2, 2);

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), size);

            IEnumerable<VertexArrayShape> gridDrawables =
                GridDrawingUtilities.GetGridDrawableFromView(snappedView, m_lineThickness, m_cellSize);
            return gridDrawables;
        }
    }
}