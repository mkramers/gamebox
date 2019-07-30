﻿using System;
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
using SFML.Graphics;
using SFML.System;

namespace GameCore
{
    public class GameRunner : IDisposable
    {
        private readonly Physics m_physics;

        public GameRunner(string _windowTitle, Vector2u _windowSize, Vector2 _gravity, float _aspectRatio)
        {
            m_renderCoreWindow = RenderCoreWindowFactory.CreateRenderCoreWindow(_windowTitle, _windowSize, _aspectRatio);

            m_keyHandlers = new TickableContainer<IKeyHandler>();

            m_physics = new Physics(_gravity);

            m_entityContainer = new DisposableTickableContainer<IEntity>();

            m_widgets = new TickableContainer<IWidget>();

            m_gameModules = new TickableContainer<IGameModule>();
        }

        private readonly DisposableTickableContainer<IEntity> m_entityContainer;
        private readonly TickableContainer<IKeyHandler> m_keyHandlers;
        private readonly RenderCoreWindow m_renderCoreWindow;
        private readonly TickableContainer<IWidget> m_widgets;
        private readonly TickableContainer<IGameModule> m_gameModules;

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
            m_gameModules.Add(_gameModule);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (m_renderCoreWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();

                m_gameModules.Tick(elapsed);

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

        public void AddFpsWidget()
        {
            WidgetFontSettings widgetFontSettingsFactory = new WidgetFontSettings();
            FontSettings fpsFontSettings = widgetFontSettingsFactory.GetSettings(WidgetFontSettingsType.FPS_COUNTER);

            Vector2 textPosition = new Vector2(fpsFontSettings.Scale, 1.0f - 1.5f * fpsFontSettings.Scale);

            Text text = TextFactory.GenerateText(fpsFontSettings);
            text.Position = textPosition.GetVector2F();

            FpsTextWidget fpsTextWidget = new FpsTextWidget(5, text);

            AddDrawableToOverlay(fpsTextWidget);

            AddWidget(fpsTextWidget);
        }
    }
}