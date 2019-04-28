using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class GridDrawingUtilities
    {
        public static ShapeAssemblyDrawable GetGridDrawableFromView(View _view)
        {
            Vector2 viewSize = _view.Size.GetVector2();
            Vector2 position = _view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.X);
            int columns = (int) Math.Round(viewSize.Y);

            ShapeAssemblyDrawable grid = DrawableFactory.GetGrid(rows, columns, viewSize, 0.05f, position);
            return grid;
        }
    }

    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly BlockingCollection<Drawable> m_drawables;

        public RenderCoreWindow(RenderWindow _renderWindow) : base(_renderWindow)
        {
            m_drawables = new BlockingCollection<Drawable>();
        }

        public bool EnableGrid { get; set; }

        public void AddDrawable(Drawable _drawable)
        {
            Debug.Assert(_drawable != null);

            m_drawables.Add(_drawable);
        }

        public override void DrawScene(RenderWindow _renderWindow)
        {
            _renderWindow.Clear(Color.Black);

            if (EnableGrid)
            {
                View view = _renderWindow.GetView();
                ShapeAssemblyDrawable gridDrawable = GridDrawingUtilities.GetGridDrawableFromView(view);

                _renderWindow.Draw(gridDrawable);

                gridDrawable.Dispose();
            }
            
            foreach (Drawable drawable in m_drawables)
            {
                _renderWindow.Draw(drawable);
            }

            _renderWindow.Display();
        }
    }
}