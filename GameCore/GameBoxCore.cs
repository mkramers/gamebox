﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using SFML.System;

namespace GameCore
{
    public class GameBoxCore : IGameBox
    {
        private readonly GameRenderWindow m_renderWindow;
        private readonly List<ITickableProvider> m_tickableProviders;
        private readonly List<IBodyProvider> m_bodyProviders;
        private readonly TickLoop m_tickLoop;
        private bool m_isPaused;
        private readonly IPhysics m_physics;

        public GameBoxCore()
        {
            m_renderWindow = new GameRenderWindow(1.0f, new Vector2u(800, 800));
            m_renderWindow.Closed += (_sender, _e) => m_tickLoop.StopLoop();

            m_tickableProviders = new List<ITickableProvider>();
            m_bodyProviders = new List<IBodyProvider>();

            m_tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            m_tickLoop.Tick += OnTick;

            m_physics = new Physics(new Vector2(0, 5.5f));
            this.AddTickable(m_physics);
        }

        public void AddWidgetProvider(IWidgetProvider _widgetProvider)
        {
            m_renderWindow.AddWidgetProvider(_widgetProvider);
        }

        public void AddBodyProvider(IBodyProvider _bodyProvider)
        {
            m_bodyProviders.Add(_bodyProvider);
        }

        public void SetTextureProvider(ITextureProvider _textureProvider)
        {
            m_renderWindow.SetTextureProvider(_textureProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_isPaused = _isPaused;
        }

        public Vector2u GetWindowSize()
        {
            return m_renderWindow.GetWindowSize();
        }

        public void AddTickableProvider(ITickableProvider _tickableProvider)
        {
            m_tickableProviders.Add(_tickableProvider);
        }

        public void StartLoop()
        {
            m_tickLoop.StartLoop();
        }

        public void Dispose()
        {
        }

        private void OnTick(object _sender, TimeElapsedEventArgs _e)
        {
            TimeSpan elapsed = _e.Elapsed;

            if (!m_isPaused)
            {
                IEnumerable<IBody> allBodies = m_bodyProviders.SelectMany(_bodyProvider => _bodyProvider.GetBodies());
                m_physics.UpdateCurrentBodies(allBodies);

                IEnumerable<ITickable> tickables =
                    m_tickableProviders.SelectMany(_provider => _provider.GetTickables());
                foreach (ITickable tickable in tickables)
                {
                    tickable.Tick(elapsed);
                }
            }

            m_renderWindow.Tick(elapsed);
        }
    }
}