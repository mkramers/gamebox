﻿using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public abstract class Game : IDisposable
    {
        private EntityContainer EntitiesContainer { get; }
        protected Physics Physics { get; }
        private TickableContainer<IKeyHandler> KeyHandlers { get; }
        protected RenderCoreWindow RenderCoreWindow { get; }

        private bool m_shouldLoopExit;

        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, viewRect);
            RenderCoreWindow.Closed += RenderWindow_OnClosed;

            KeyHandlers = new TickableContainer<IKeyHandler>();

            Vector2 gravity = new Vector2(0, 10);
            Physics = new Physics(gravity);
            
            EntitiesContainer = new EntityContainer();
        }

        public void Dispose()
        {
            RenderCoreWindow.Dispose();
            Physics.Dispose();

            EntitiesContainer.Dispose();
        }

        protected void AddKeyHandler(IKeyHandler _keyHandler)
        {
            KeyHandlers.Add(_keyHandler);
        }

        private void RenderWindow_OnClosed(object _sender, EventArgs _e)
        {
            m_shouldLoopExit = true;
        }

        protected void AddEntity(IEntity _entity)
        {
            EntitiesContainer.Add(_entity);

            RenderCoreWindow.Add(_entity);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                //the following order is important

                KeyHandlers.Tick(elapsed);

                Physics.Tick(elapsed);

                EntitiesContainer.Tick(elapsed);

                RenderCoreWindow.Tick(elapsed);

                //too fast!
                Thread.Sleep(30);

                if (m_shouldLoopExit)
                {
                    break;
                }
            }
        }
    }
}