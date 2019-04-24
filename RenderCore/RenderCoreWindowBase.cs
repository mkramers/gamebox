using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : IRenderCoreWindow
    {
        private readonly RenderWindow m_renderWindow;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
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