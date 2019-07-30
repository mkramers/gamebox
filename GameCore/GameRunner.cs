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
            m_renderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, _aspectRatio);

            m_keyHandlers = new TickableContainer<IKeyHandler>();

            m_physics = new Physics(_gravity);

            m_entityContainer = new DisposableTickableContainer<IEntity>();

            m_widgets = new TickableContainer<IWidget>();
        }

        private readonly DisposableTickableContainer<IEntity> m_entityContainer;
        private readonly TickableContainer<IKeyHandler> m_keyHandlers;
        private readonly RenderCoreWindow m_renderCoreWindow;
        private readonly TickableContainer<IWidget> m_widgets;

        public void Dispose()
        {
            m_renderCoreWindow.Dispose();
            m_physics.Dispose();

            m_entityContainer.Dispose();
        }

        protected Physics GetPhysics()
        {
            return m_physics;
        }

        protected void AddEntity(IEntity _entity)
        {
            m_entityContainer.Add(_entity);

            AddDrawableToScene(_entity);
        }

        protected void AddDrawableToScene(IDrawable _entity)
        {
            IRenderCoreTarget scene = m_renderCoreWindow.GetScene();
            scene.AddDrawable(_entity);
        }

        protected void AddDrawableToOverlay(IDrawable _entity)
        {
            IRenderCoreTarget scene = m_renderCoreWindow.GetOverlay();
            scene.AddDrawable(_entity);
        }

        protected void AddWidget(IWidget _widget)
        {
            m_widgets.Add(_widget);
        }

        protected void SetSceneViewProvider(ViewProviderBase _viewProvider)
        {
            RenderCoreWindow renderCoreWindow = m_renderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            scene.SetViewProvider(_viewProvider);
        }

        protected void AddKeyHandler(KeyHandler _keyHandler)
        {
            m_keyHandlers.Add(_keyHandler);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (m_renderCoreWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                m_keyHandlers.Tick(elapsed);

                m_physics.Tick(elapsed);

                m_entityContainer.Tick(elapsed);

                m_widgets.Tick(elapsed);

                m_renderCoreWindow.Tick(elapsed);

                DelayLoop();
            }
        }

        private static void DelayLoop()
        {
            Thread.Sleep(30);
        }
    }
}