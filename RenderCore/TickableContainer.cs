using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Input;
using BepuUtilities.Memory;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public abstract class Game : IDisposable
    {
        protected readonly TickableContainer m_tickableContainer;
        protected readonly RenderCoreWindow m_renderCoreWindow;
        protected readonly EntityPhysics m_entityPhysics;
        private BufferPool m_bufferPool;

        public Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, viewRect);
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) { IsDrawEnabled = true };

            m_renderCoreWindow = new RenderCoreWindow(renderWindow, new[] { gridWidget });

            m_tickableContainer = new TickableContainer();

            m_bufferPool = new BufferPool();
            m_entityPhysics = new EntityPhysics(m_bufferPool);

            //order matters
            m_tickableContainer.Add(m_entityPhysics);
            m_tickableContainer.Add(m_renderCoreWindow);
        }

        public void StartLoop()
        {
            while (true)
            {
                m_tickableContainer.Tick();
            }
        }

        public abstract void CreateMainCharacter();

        public void Dispose()
        {
            m_bufferPool.Clear();
            m_renderCoreWindow.Dispose();
            m_entityPhysics.Dispose();
        }
    }

    public class TickableContainer
    {
        private readonly Stopwatch m_stopwatch;
        private readonly BlockingCollection<ITickable> m_tickables;

        public TickableContainer()
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
}