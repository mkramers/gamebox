using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities(IPhysics _physics);
    }
    
    public abstract class Game : IDisposable, ITickable
    {
        private readonly List<IEntity> m_entities;
        protected EntityPhysics EntityPhysics { get; }
        private readonly TickableContainer<IKeyHandler> m_keyHandlers;
        private readonly TickableContainer<ITickable> m_objectFramework;
        protected RenderCoreWindow RenderCoreWindow { get; }
        private bool m_shouldLoopExit;

        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, viewRect);
            RenderCoreWindow.Closed += RenderWindow_OnClosed;

            m_objectFramework = new TickableContainer<ITickable>();

            m_keyHandlers = new TickableContainer<IKeyHandler>();

            Vector2 gravity = new Vector2(0, 10);
            EntityPhysics = new EntityPhysics(gravity);

            //order matters
            m_objectFramework.Add(EntityPhysics);
            m_objectFramework.Add(RenderCoreWindow);
            m_objectFramework.Add(m_keyHandlers);

            m_entities = new List<IEntity>();
        }

        public void SetGravity(Vector2 _gravity)
        {
            EntityPhysics.SetGravity(_gravity);
        }

        public void Dispose()
        {
            RenderCoreWindow.Dispose();
            EntityPhysics.Dispose();

            foreach (IEntity entity in m_entities)
            {
                entity.Dispose();
            }

            m_entities.Clear();
        }

        public abstract void Tick(TimeSpan _elapsed);
        
        protected void AddKeyHandler(IKeyHandler _keyHandler)
        {
            m_keyHandlers.Add(_keyHandler);
        }
        
        private void RenderWindow_OnClosed(object _sender, EventArgs _e)
        {
            m_shouldLoopExit = true;
        }

        protected void AddEntity(IEntity _entity)
        {
            m_entities.Add(_entity);

            EntityPhysics.Add(_entity);
            RenderCoreWindow.Add(_entity);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                m_objectFramework.Tick(stopwatch.Elapsed);

                Tick(stopwatch.Elapsed);

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