using System;
using System.Numerics;
using Common.Tickable;
using PhysicsCore;
using RenderCore.ViewProvider;
using TGUI;

namespace RenderBox.New
{
    public class GameBox : IGameBox
    {
        private readonly IGameBox m_gameBox;
        private readonly IPhysics m_physics;

        public GameBox()
        {
            m_gameBox = new GameBoxCore();

            m_physics = new Physics(new Vector2(0, 5.5f));
            AddTickable(m_physics);
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

        public void InvokeGui(Action<Gui> _guiAction)
        {
            m_gameBox.InvokeGui(_guiAction);
        }

        public void AddDrawableProvider(IDrawableProvider _drawableProvider)
        {
            m_gameBox.AddDrawableProvider(_drawableProvider);
        }

        public void AddTickable(ITickable _tickable)
        {
            m_gameBox.AddTickable(_tickable);
        }

        public void InvokePhysics(Action<IPhysics> _action)
        {
            _action(m_physics);
        }

        public void Dispose()
        {
            m_gameBox.Dispose();
            m_physics.Dispose();
        }
    }
}