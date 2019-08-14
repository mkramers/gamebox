using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ViewProvider;
using SFML.Graphics;
using TGUI;

namespace GameCore
{
    public abstract class GameBase : IGameProvider, IViewProvider
    {
        protected readonly List<IDrawable> m_drawables;
        private readonly List<IGameProvider> m_gameProviders;

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly Gui m_gui;

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly IPhysics m_physics;

        protected readonly List<ITickable> m_tickables;
        protected IViewProvider m_viewProvider;

        protected GameBase(IPhysics _physics, Gui _gui)
        {
            m_physics = _physics;
            m_gui = _gui;
            m_drawables = new List<IDrawable>();
            m_tickables = new List<ITickable>();
            m_gameProviders = new List<IGameProvider>();
        }

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;

        protected void AddGameProvider(IGameProvider _gameProvider)
        {
            _gameProvider.PauseGame += OnPausedGame;
            _gameProvider.ResumeGame += OnResumedGame;

            m_gameProviders.Add(_gameProvider);
        }

        private void OnResumedGame(object _sender, EventArgs _e)
        {
            ResumeGame?.Invoke(_sender, _e);
        }

        private void OnPausedGame(object _sender, EventArgs _e)
        {
            PauseGame?.Invoke(_sender, _e);
        }

        protected void RemoveGameProvider(IGameProvider _gameProvider)
        {
            _gameProvider.PauseGame -= OnPausedGame;
            _gameProvider.ResumeGame -= OnResumedGame;

            m_gameProviders.Remove(_gameProvider);
        }

        public virtual View GetView()
        {
            return m_viewProvider.GetView();
        }

        public virtual IEnumerable<IDrawable> GetDrawables()
        {
            List<IDrawable> drawables = new List<IDrawable>();
            drawables.AddRange(m_drawables);

            IEnumerable<IDrawable> subGameDrawables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetDrawables());
            drawables.AddRange(subGameDrawables);

            return drawables;
        }

        public virtual IEnumerable<ITickable> GetTickables()
        {
            List<ITickable> tickables = new List<ITickable>();
            tickables.AddRange(m_tickables);

            IEnumerable<ITickable> subGameTickables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetTickables());
            tickables.AddRange(subGameTickables);

            return tickables;
        }
        
        public virtual void Dispose()
        {
        }
    }
}