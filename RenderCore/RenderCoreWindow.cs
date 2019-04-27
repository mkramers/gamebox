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
        private readonly PhysicsController m_physicsController;

        public RenderCoreWindow(RenderWindow _renderWindow, PhysicsController _physicsController) : base(_renderWindow)
        {
            m_drawables = new List<Drawable>();
            m_physicsController = _physicsController;
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

            m_physicsController.ResolvePhysics();

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

            int rows = (int)Math.Round(viewSize.X);
            int columns = (int)Math.Round(viewSize.Y);

            Drawable grid = DrawableFactory.GetGrid(rows, columns, viewSize, 0.05f, position);

            AddDrawable(grid);
        }

        public bool EnableGrid { get; set; }
    }
}