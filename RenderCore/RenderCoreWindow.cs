using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderCore
{
    public class RenderCoreWindow : ITickable, IDisposable
    {
        private readonly IRenderCoreTarget m_overlayTarget;
        private readonly RenderWindow m_renderWindow;
        private readonly float m_aspectRatio;
        private readonly IRenderCoreTarget m_sceneTarget;

        public RenderCoreWindow(RenderWindow _renderWindow, float _aspectRatio)
        {
            m_renderWindow = _renderWindow;
            m_aspectRatio = _aspectRatio;
            m_renderWindow.Resized += OnRenderWindowResized;
            m_renderWindow.Closed += (_sender, _e) => m_renderWindow.Close();

            Vector2u windowSize = m_renderWindow.Size;

            m_sceneTarget = new RenderCoreTarget(windowSize, new Color(40, 40, 40));
            m_overlayTarget = new RenderCoreTarget(windowSize, Color.Transparent);

            View view = new View(new FloatRect(0f, 0, 1, 1));
            ViewProviderBase viewProviderBase = new ViewProviderBase(view);

            m_overlayTarget.SetViewProvider(viewProviderBase);

            Resize(m_renderWindow.Size);
        }

        private void OnRenderWindowResized(object _sender, SizeEventArgs _e)
        {
            uint width = _e.Width;
            uint height = _e.Height;

            Resize(new Vector2u(width, height));
        }

        private void Resize(Vector2u _windowSize)
        {
            float windowAspectRatio = (float)_windowSize.X / _windowSize.Y;

            if (windowAspectRatio <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowAspectRatio), "negative aspect ratio not supported");
            }

            FloatRect viewPort = new FloatRect(0, 0, 1, 1);

            if (windowAspectRatio > m_aspectRatio)
            {
                float xPadding = (windowAspectRatio - m_aspectRatio) / 2.0f;
                viewPort = new FloatRect(xPadding / 2.0f, 0, 1 - xPadding, 1);
            }
            else if (windowAspectRatio < m_aspectRatio)
            {
                float yPadding = (m_aspectRatio - windowAspectRatio) / 2.0f;
                viewPort = new FloatRect(0, yPadding / 2.0f, 0, 1 - yPadding);
            }
            else if (Math.Abs(windowAspectRatio - m_aspectRatio) < 0.0001f)
            {
                viewPort = new FloatRect(0, 0, 1, 1);
            }

            View renderWindowView = m_renderWindow.GetView();
            renderWindowView.Viewport = viewPort;

            m_renderWindow.SetView(renderWindowView);
        }

        public bool IsOpen => m_renderWindow.IsOpen;

        public void Dispose()
        {
            m_renderWindow.Dispose();

            m_sceneTarget.Dispose();
            m_overlayTarget.Dispose();
        }

        public void Tick(TimeSpan _elapsed)
        {
            if (!m_renderWindow.IsOpen)
            {
                return;
            }

            m_renderWindow.DispatchEvents();

            m_sceneTarget.Tick(_elapsed);
            m_overlayTarget.Tick(_elapsed);

            Draw(m_renderWindow);
        }

        public IRenderCoreTarget GetScene()
        {
            return m_sceneTarget;
        }

        public IRenderCoreTarget GetOverlay()
        {
            return m_overlayTarget;
        }

        private void Draw(RenderWindow _renderWindow)
        {
            _renderWindow.Clear();

            _renderWindow.Draw(m_sceneTarget);
            _renderWindow.Draw(m_overlayTarget);

            _renderWindow.Display();
        }
    }
}