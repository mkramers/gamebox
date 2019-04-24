using System;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : IRenderCoreWindow
    {
        private readonly RenderWindow m_renderWindow;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;
        }

        private void RenderWindowOnClosed(object _sender, EventArgs _e)
        {
            RenderWindow window = _sender as RenderWindow;
            Debug.Assert(window != null);

            window.Close();
        }

        public void StartRenderLoop()
        {
            while (m_renderWindow.IsOpen)
            {
                m_renderWindow.DispatchEvents();

                DrawScene(m_renderWindow);
            }
        }

        public abstract void DrawScene(RenderWindow _renderWindow);
    }
}