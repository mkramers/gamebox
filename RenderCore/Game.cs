using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public abstract class Game : IDisposable
    {
        private bool m_shouldLoopExit;

        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            FloatRect viewRect = new FloatRect(-10, 10, 20, 20);

            RenderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, viewRect);
            RenderCoreWindow.Closed += (_sender, _e) => m_shouldLoopExit = true;

            RenderCoreWindow.AddWidget(new GridWidget());

            KeyHandlers = new TickableContainer<IKeyHandler>();

            Vector2 gravity = new Vector2(0, 10);
            Physics = new Physics(gravity);

            EntitiesContainer = new EntityContainer();
        }

        private EntityContainer EntitiesContainer { get; }
        protected Physics Physics { get; }
        protected TickableContainer<IKeyHandler> KeyHandlers { get; }
        protected RenderCoreWindow RenderCoreWindow { get; }

        public void Dispose()
        {
            RenderCoreWindow.Dispose();
            Physics.Dispose();

            EntitiesContainer.Dispose();
        }

        protected void AddMap(IMap _map, IPhysics _physics)
        {
            foreach (IEntity woodEntity in _map.GetEntities(_physics))
            {
                AddEntity(woodEntity);
            }
        }

        protected void AddEntity(IEntity _entity)
        {
            EntitiesContainer.Add(_entity);

            RenderCoreWindow.Add(_entity);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                //the following order is important

                KeyHandlers.Tick(elapsed);

                Physics.Tick(elapsed);

                EntitiesContainer.Tick(elapsed);

                RenderCoreWindow.Tick(elapsed);

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