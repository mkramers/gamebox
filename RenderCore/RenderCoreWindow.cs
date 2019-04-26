using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly List<Drawable> m_drawables;
        private readonly float m_tileSize;

        public RenderCoreWindow(RenderWindow _renderWindow, float _tileSize) : base(_renderWindow)
        {
            m_tileSize = _tileSize;
            m_drawables = new List<Drawable>();
        }

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
            int rows = (int)Math.Round(1.0f / m_tileSize);
            int columns = (int)Math.Round(1.0f / m_tileSize);
            Drawable grid = DrawableFactory.GetGrid(rows, columns, Vector2.One, 0.005f, Vector2.Zero);

            AddDrawable(grid);
        }

        public bool EnableGrid { get; set; }
    }
}