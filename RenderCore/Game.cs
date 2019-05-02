using System;
using System.Numerics;
using System.Threading;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public abstract class Game : IDisposable
    {
        protected readonly EntityPhysics m_entityPhysics;
        protected readonly RenderCoreWindow m_renderCoreWindow;
        private readonly TickableContainer m_tickableContainer;

        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, viewRect);
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) {IsDrawEnabled = true};

            m_renderCoreWindow = new RenderCoreWindow(renderWindow, new[] {gridWidget});

            m_tickableContainer = new TickableContainer();

            Vector2 gravity = new Vector2(0, 10);
            m_entityPhysics = new EntityPhysics(gravity);

            //order matters
            m_tickableContainer.Add(m_entityPhysics);
            m_tickableContainer.Add(m_renderCoreWindow);
        }

        public void Dispose()
        {
            m_renderCoreWindow.Dispose();
            m_entityPhysics.Dispose();
        }

        public void StartLoop()
        {
            while (true)
            {
                m_tickableContainer.Tick();
                Thread.Sleep(30);
            }
        }
    }
}