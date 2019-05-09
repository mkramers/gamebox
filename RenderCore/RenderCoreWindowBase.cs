using System;
using System.Diagnostics;
using Aether.Physics2D.Common.Maths;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : ITickable
    {
        protected readonly RenderWindow m_renderWindow;
        private IViewController m_viewController;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;
            m_renderWindow.Resized += RenderWindowOnResized;
        }

        public virtual void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            m_viewController.Tick(_elapsed);

            View view = m_viewController.GetView();
            m_renderWindow.SetView(view);

            DrawScene(m_renderWindow);
        }

        public void SetViewController(IViewController _viewController)
        {
            m_viewController = _viewController;
        }

        private static void RenderWindowOnClosed(object _sender, EventArgs _e)
        {
            RenderWindow window = _sender as RenderWindow;
            Debug.Assert(window != null);

            window.Close();
        }

        private void RenderWindowOnResized(object _sender, SizeEventArgs _e)
        {
            Vector2u windowSize = new Vector2u(_e.Width, _e.Height);
            m_viewController.SetParentSize(windowSize);
        }

        protected abstract void DrawScene(RenderWindow _renderWindow);

        public event EventHandler Closed
        {
            add => m_renderWindow.Closed += value;
            remove => m_renderWindow.Closed -= value;
        }
    }
}