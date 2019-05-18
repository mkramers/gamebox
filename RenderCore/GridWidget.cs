using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridWidget : MultiDrawable<Shape>, ITickablePositionDrawable
    {
        private readonly IViewProvider m_viewProvider;

        public GridWidget(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }
        
        public void Tick(TimeSpan _elapsed)
        {
            View view = m_viewProvider.GetView();

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), view.Size);

            MultiDrawable<Shape> gridDrawable = GridDrawingUtilities.GetGridDrawableFromView(snappedView);

            DisposeItemsAndClear();
            AddRange(gridDrawable);
        }
    }
}