using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using Common.Extensions;
using Common.Tickable;
using GameCore.Entity;
using GameCore.Input.Key;
using LibExtensions;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.System;
using TGUI;

namespace GameCore
{
    public class GameRunner : IDisposable
    {
        private readonly DisposableTickableContainer<IEntity> m_entityContainer;
        private readonly TickableContainer<IGameModule> m_gameModules;
        private readonly TickableContainer<IKeyHandler> m_keyHandlers;
        private readonly Physics m_physics;
        private readonly RenderCoreWindow m_renderCoreWindow;
        private readonly TickableContainer<IWidget> m_widgets;
        private bool m_pauseGame;

        public GameRunner(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio)
        {
            m_renderCoreWindow =
                RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, _aspectRatio);

            m_keyHandlers = new TickableContainer<IKeyHandler>();

            m_physics = new Physics(_gravity);

            m_entityContainer = new DisposableTickableContainer<IEntity>();

            m_widgets = new TickableContainer<IWidget>();

            m_gameModules = new TickableContainer<IGameModule>();
        }

        public void Dispose()
        {
            m_renderCoreWindow.Dispose();
            m_physics.Dispose();

            m_entityContainer.Dispose();
        }

        public Physics GetPhysics()
        {
            return m_physics;
        }

        public void AddEntity(IEntity _entity)
        {
            m_entityContainer.Add(_entity);

            AddDrawableToScene(_entity);
        }

        public void AddDrawableToScene(IDrawable _entity)
        {
            IRenderCoreTarget scene = m_renderCoreWindow.GetScene();
            scene.AddDrawable(_entity);
        }

        public void AddDrawableToOverlay(IDrawable _entity)
        {
            IRenderCoreTarget scene = m_renderCoreWindow.GetOverlay();
            scene.AddDrawable(_entity);
        }

        public void AddWidget(IWidget _widget)
        {
            m_widgets.Add(_widget);
        }

        public void SetSceneViewProvider(ViewProviderBase _viewProvider)
        {
            RenderCoreWindow renderCoreWindow = m_renderCoreWindow;
            IRenderCoreTarget scene = renderCoreWindow.GetScene();

            scene.SetViewProvider(_viewProvider);
        }

        public void AddKeyHandler(KeyHandler _keyHandler)
        {
            m_keyHandlers.Add(_keyHandler);
        }

        public void AddGameModule(IGameModule _gameModule)
        {
            _gameModule.PauseGame += (_sender, _e) => m_pauseGame = true;
            _gameModule.ResumeGame += (_sender, _e) => m_pauseGame = false;

            m_gameModules.Add(_gameModule);
        }

        public Gui GetGui()
        {
            return m_renderCoreWindow.GetGui();
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (m_renderCoreWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                if (!m_pauseGame)
                {
                    m_keyHandlers.Tick(elapsed);

                    m_physics.Tick(elapsed);

                    m_entityContainer.Tick(elapsed);

                    m_widgets.Tick(elapsed);
                }

                m_gameModules.Tick(elapsed);

                m_renderCoreWindow.Tick(elapsed);

                DelayLoop();
            }
        }

        private static void DelayLoop()
        {
            Thread.Sleep(30);
        }

        public void AddFpsWidget()
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2u windowSize = m_renderCoreWindow.GetWindowSize();

            Vector2 textPosition = new Vector2(windowSize.X / 2.0f, windowSize.Y - fpsFontSettings.Size);

            FpsLabel fpsWidget = new FpsLabel(5, fpsFontSettings)
            {
                Position = textPosition.GetVector2F()
            };

            Gui gui = m_renderCoreWindow.GetGui();
            gui.Add(fpsWidget);

            AddWidget(fpsWidget);
        }

        public IRenderCoreTarget GetScene()
        {
            return m_renderCoreWindow.GetScene();
        }
    }
}