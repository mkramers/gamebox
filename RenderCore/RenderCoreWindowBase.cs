using System;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : ITickable
    {
        protected readonly RenderWindow m_renderWindow;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            DrawScene(m_renderWindow);
        }

        private static void RenderWindowOnClosed(object _sender, EventArgs _e)
        {
            RenderWindow window = _sender as RenderWindow;
            Debug.Assert(window != null);

            window.Close();
        }

        protected abstract void DrawScene(RenderWindow _renderWindow);
    }
}