using System;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class RenderCoreWindow : ITickable, IDisposable
    {
        private readonly IRenderCoreTarget m_overlayTarget;
        private readonly RenderWindow m_renderWindow;
        private readonly IRenderCoreTarget m_sceneTarget;

        public RenderCoreWindow(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += (_sender, _e) => m_renderWindow.Close();

            Vector2u windowSize = m_renderWindow.Size;

            m_sceneTarget = new RenderCoreTarget(windowSize, Color.Black);
            m_overlayTarget = new RenderCoreTarget(windowSize, Color.Transparent);
            View view = new View(new FloatRect(0f, 0, 1, 1));
            ViewProviderBase viewProviderBase = new ViewProviderBase(view);
            m_overlayTarget.SetViewProvider(viewProviderBase);
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