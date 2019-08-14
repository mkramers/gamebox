using System.Numerics;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.System;
using TGUI;

namespace GameCore
{
    public class GameBox : IGameBox
    {
        private readonly IGameBox m_gameBox;
        private readonly IPhysics m_physics;

        public GameBox()
        {
            m_gameBox = new GameBoxCore();

            m_physics = new Physics(new Vector2(0, 5.5f));
            m_gameBox.AddTickable(m_physics);
        }

        public void StartLoop()
        {
            m_gameBox.StartLoop();
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_gameBox.SetViewProvider(_viewProvider);
        }

        public void SetIsPaused(bool _isPaused)
        {
            m_gameBox.SetIsPaused(_isPaused);
        }

        public Gui GetGui()
        {
            return m_gameBox.GetGui();
        }

        public Vector2u GetWindowSize()
        {
            return m_gameBox.GetWindowSize();
        }

        public void AddTickableProvider(TickableProvider _tickableProvider)
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
            m_physics.Dispose();
        }

        public IPhysics GetPhysics()
        {
            return m_physics;
        }
    }
}