﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Windows.Input;
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

        public Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, viewRect);
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) { IsDrawEnabled = true };

            m_renderCoreWindow = new RenderCoreWindow(renderWindow, new[] { gridWidget });

            m_tickableContainer = new TickableContainer();

            Vector2 gravity = new Vector2(0, 10);
            m_entityPhysics = new EntityPhysics(gravity);

            //order matters
            m_tickableContainer.Add(m_entityPhysics);
            m_tickableContainer.Add(m_renderCoreWindow);
        }

        public void StartLoop()
        {
            while (true)
            {
                m_tickableContainer.Tick();
                Thread.Sleep(30);
            }
        }

        public abstract void CreateMainCharacter();

        public void Dispose()
        {
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
            TimeSpan elapsed = m_stopwatch.Elapsed;

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