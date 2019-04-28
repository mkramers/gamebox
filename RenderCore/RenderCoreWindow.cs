using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using SFML.Graphics;

namespace RenderCore
{
    public interface ITickable
    {
        void Tick(long _elapsedMs);
    }

    public class ObjectFramework
    {
        private readonly BlockingCollection<ITickable> m_tickables;
        private readonly Stopwatch m_stopwatch;
        
        public ObjectFramework()
        {
            m_tickables = new BlockingCollection<ITickable>();
            
            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
        }

        public void Tick()
        {
            long elapsed = m_stopwatch.ElapsedMilliseconds;

            foreach (ITickable tickable in m_tickables)
            {
                tickable.Tick(elapsed);
            }

            m_stopwatch.Restart();
        }

        public void Add(ITickable _tickable)
        {
            m_tickables.Add(_tickable);
        }
    }

    public class RenderCoreWindow : RenderCoreWindowBase
    {
        private readonly BlockingCollection<Drawable> m_drawables;
        private readonly PhysicsController m_physicsController;

        public RenderCoreWindow(RenderWindow _renderWindow, PhysicsController _physicsController) : base(_renderWindow)
        {
            m_drawables = new BlockingCollection<Drawable>();
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