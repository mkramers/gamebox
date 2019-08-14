using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using SFML.System;
using TGUI;

namespace GameCore
{
    public class GameBoxCore : IGameBox
    {
        private readonly SubmitToDrawRenderWindow m_renderWindow;
        private readonly List<ITickableProvider> m_tickableProviders;
        private readonly TickLoop m_tickLoop;
        private bool m_isPaused;

        public GameBoxCore()
        {
            m_renderWindow = new SubmitToDrawRenderWindow(1.0f, new Vector2u(800, 800));
            m_renderWindow.Closed += (_sender, _e) => m_tickLoop.StopLoop();

            m_tickableProviders = new List<ITickableProvider>();

            m_tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            m_tickLoop.Tick += OnTick;
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_renderWindow.AddDrawableProvider(_drawableProvider);
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_renderWindow.SetViewProvider(_viewProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_isPaused = _isPaused;
        }

        public Gui GetGui()
        {
            return m_renderWindow.GetGui();
        }

        public Vector2u GetWindowSize()
        {
            return m_renderWindow.GetWindowSize();
        }

        public void AddTickableProvider(TickableProvider _tickableProvider)
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