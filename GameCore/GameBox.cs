using System.Numerics;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;

namespace GameCore
{
    public class GameBox : IGameBox
    {
        private readonly IGameBox m_gameBox;

        public GameBox()
        {
            m_gameBox = new GameBoxCore();
        }

        public void StartLoop()
        {
            m_gameBox.StartLoop();
        }

        public void AddWidgetProvider(IWidgetProvider _widgetProvider)
        {
            m_gameBox.AddWidgetProvider(_widgetProvider);
        }

        public void AddBodyProvider(IBodyProvider _bodyProvider)
        {
            m_gameBox.AddBodyProvider(_bodyProvider);
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_gameBox.SetViewProvider(_viewProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_gameBox.SetIsPaused(_isPaused);
        }

        public Vector2u GetWindowSize()
        {
            return m_gameBox.GetWindowSize();
        }

        public void AddTickableProvider(ITickableProvider _tickableProvider)
        {
            m_gameBox.AddTickableProvider(_tickableProvider);
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_gameBox.AddDrawableProvider(_drawableProvider);
        }

        public void Dispose()
        {
            m_gameBox.Dispose();
        }
    }
}