using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly TickableContainer m_objectFramework;
        private readonly List<IEntity> m_entities;
        protected readonly TickableContainer m_keyHandlers; 
        private bool m_shouldLoopExit;

        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow(_windowTitle, _windowSize, viewRect);
            renderWindow.Closed+= RenderWindow_OnClosed;
            GridWidget gridWidget = new GridWidget(renderWindow.GetView()) {IsDrawEnabled = true};

            m_renderCoreWindow = new RenderCoreWindow(renderWindow, new[] {gridWidget});

            m_objectFramework = new TickableContainer();

            m_keyHandlers =  new TickableContainer();

            Vector2 gravity = new Vector2(0, 10);
            m_entityPhysics = new EntityPhysics(gravity);

            //order matters
            m_objectFramework.Add(m_entityPhysics);
            m_objectFramework.Add(m_renderCoreWindow);
            m_objectFramework.Add(m_keyHandlers);

            m_entities = new List<IEntity>();
        }

        private void RenderWindow_OnClosed(object _sender, EventArgs _e)
        {
            m_shouldLoopExit = true;
        }

        protected void AddEntity(IEntity _entity)
        {
            m_entities.Add(_entity);
            
            m_entityPhysics.Add(_entity);
            m_renderCoreWindow.Add(_entity);
        }

        public void Dispose()
        {
            m_renderCoreWindow.Dispose();
            m_entityPhysics.Dispose();

            foreach (IEntity entity in m_entities)
            {
                entity.Dispose();
            }
            m_entities.Clear();
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                m_objectFramework.Tick(stopwatch.Elapsed);

                stopwatch.Restart();

                //too fast!
                Thread.Sleep(30);
                
                if (m_shouldLoopExit)
                {
                    break;
                }
            }
        }
    }
}