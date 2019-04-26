using System;
using System.Diagnostics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : IRenderCoreWindow
    {
        protected readonly RenderWindow m_renderWindow;
        private readonly RenderCoreWindowKeyboardHandler m_coreWindowInputHandler;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;

            //m_coreWindowInputHandler = new RenderCoreWindowKeyboardHandler(m_renderWindow);
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