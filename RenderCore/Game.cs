using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using SFML.System;

namespace RenderCore
{
    public abstract class Game : IDisposable
    {
        protected Game(string _windowTitle, Vector2u _windowSize)
        {
            RenderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize);

            KeyHandlers = new TickableContainer<IKeyHandler>();

            Vector2 gravity = new Vector2(0, 5);
            Physics = new Physics(gravity);

            EntityContainer = new DisposableTickableContainer<IEntity>();

            Widgets = new TickableContainer<ITickable>();
        }

        private DisposableTickableContainer<IEntity> EntityContainer { get; }
        protected Physics Physics { get; }
        protected TickableContainer<IKeyHandler> KeyHandlers { get; }
        protected RenderCoreWindow RenderCoreWindow { get; }

        private TickableContainer<ITickable> Widgets { get; }

        public void Dispose()
        {
            RenderCoreWindow.Dispose();
            Physics.Dispose();

            EntityContainer.Dispose();
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
            EntityContainer.Add(_entity);

            IRenderCoreTarget scene = RenderCoreWindow.GetScene();
            scene.AddDrawable(_entity);
        }

        protected void AddWidget(ITickable _widget)
        {
            Widgets.Add(_widget);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (RenderCoreWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                //the following order is important

                KeyHandlers.Tick(elapsed);

                Physics.Tick(elapsed);

                EntityContainer.Tick(elapsed);

                Widgets.Tick(elapsed);

                RenderCoreWindow.Tick(elapsed);

                //too fast!
                Thread.Sleep(30);
            }
        }
    }
}