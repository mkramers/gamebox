using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using Common.Extensions;
using Common.Tickable;
using GameCore.Entity;
using GameCore.Input.Key;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.System;

namespace GameCore
{
    public abstract class GameRunner : IDisposable
    {
        private readonly Physics m_physics;

        protected GameRunner(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio)
        {
            RenderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, _aspectRatio);

            KeyHandlers = new TickableContainer<IKeyHandler>();

            m_physics = new Physics(_gravity);

            EntityContainer = new DisposableTickableContainer<IEntity>();

            Widgets = new TickableContainer<IWidget>();
        }

        private DisposableTickableContainer<IEntity> EntityContainer { get; }
        private TickableContainer<IKeyHandler> KeyHandlers { get; }
        private RenderCoreWindow RenderCoreWindow { get; }
        private TickableContainer<IWidget> Widgets { get; }

        public void Dispose()
        {
            RenderCoreWindow.Dispose();
            m_physics.Dispose();

            EntityContainer.Dispose();
        }

        protected Physics GetPhysics()
        {
            return m_physics;
        }

        protected void AddEntity(IEntity _entity)
        {
            EntityContainer.Add(_entity);

            AddDrawableToScene(_entity);
        }

        protected void AddDrawableToScene(IDrawable _entity)
        {
            IRenderCoreTarget scene = RenderCoreWindow.GetScene();
            scene.AddDrawable(_entity);
        }

        protected void AddDrawableToOverlay(IDrawable _entity)
        {
            IRenderCoreTarget scene = RenderCoreWindow.GetOverlay();
            scene.AddDrawable(_entity);
        }

        protected void AddWidget(IWidget _widget)
        {
            Widgets.Add(_widget);
        }

        protected void SetSceneViewProvider(ViewProviderBase _viewProvider)
        {
            RenderCoreWindow renderCoreWindow = RenderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            scene.SetViewProvider(_viewProvider);
        }

        protected void AddKeyHandler(KeyHandler _keyHandler)
        {
            KeyHandlers.Add(_keyHandler);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (RenderCoreWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                KeyHandlers.Tick(elapsed);

                m_physics.Tick(elapsed);

                EntityContainer.Tick(elapsed);

                Widgets.Tick(elapsed);

                RenderCoreWindow.Tick(elapsed);

                DelayLoop();
            }
        }

        private static void DelayLoop()
        {
            Thread.Sleep(30);
        }
    }
}