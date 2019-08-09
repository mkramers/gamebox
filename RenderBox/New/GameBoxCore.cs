using System;
using Common.Tickable;
using RenderCore.ViewProvider;
using SFML.System;
using TGUI;

namespace RenderBox.New
{
    public class GameBoxCore : IGameBox
    {
        private readonly SubmitToDrawRenderWindow m_renderWindow;
        private readonly TickLoop m_tickLoop;
        private readonly TickableContainer<ITickable> m_tickables;
        private bool m_isPaused;

        public GameBoxCore()
        {
            m_renderWindow = new SubmitToDrawRenderWindow(1.0f, new Vector2u(800, 800));
            m_renderWindow.Closed += (_sender, _e) => m_tickLoop.StopLoop();

            m_tickables = new TickableContainer<ITickable>();

            m_tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            m_tickLoop.Tick += OnTick;
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_renderWindow.AddDrawableProvider(_drawableProvider);
        }

        public void AddTickable(ITickable _tickable)
        {
            m_tickables.Add(_tickable);
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_renderWindow.SetViewProvider(_viewProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_isPaused = _isPaused;
        }

        public void InvokeGui(Action<Gui> _guiAction)
        {
            m_renderWindow.InvokeGui(_guiAction);
        }

        public Vector2u GetWindowSize()
        {
            return m_renderWindow.GetWindowSize();
        }

        private void OnTick(object _sender, TimeElapsedEventArgs _e)
        {
            TimeSpan elapsed = _e.Elapsed;

            if (!m_isPaused)
            {
                m_tickables.Tick(elapsed);
            }

            m_renderWindow.Tick(elapsed);
        }

        public void StartLoop()
        {
            m_tickLoop.StartLoop();
        }

        public void Dispose()
        {
            m_tickables.Dispose();
        }
    }
}