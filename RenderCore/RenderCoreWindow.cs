using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
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
                DrawGrid();
            }
            
            foreach (Drawable drawable in m_drawables)
            {
                _renderWindow.Draw(drawable);
            }

            _renderWindow.Display();
        }

        private void DrawGrid()
        {
            View view = m_renderWindow.GetView();

            Vector2 viewSize = view.Size.GetVector2();
            Vector2 position = view.Center.GetVector2() - viewSize / 2.0f;

            int rows = (int) Math.Round(viewSize.X);
            int columns = (int) Math.Round(viewSize.Y);

            Drawable grid = DrawableFactory.GetGrid(rows, columns, viewSize, 0.05f, position);

            AddDrawable(grid);
        }

        public void SetViewCenter(Vector2 _center)
        {
            View view = m_renderWindow.GetView();

            view.Center = _center.GetVector2f();
        }
    }
}