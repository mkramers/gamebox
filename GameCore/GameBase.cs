using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using SFML.Graphics;
using TGUI;

namespace GameCore
{
    public abstract class GameBase : IGameProvider, IViewProvider, IDrawable
    {
        protected readonly List<IDrawable> m_drawables;
        private readonly List<IGameProvider> m_gameProviders;
        protected readonly List<Widget> m_widgets;
        protected readonly List<IBody> m_bodies;
        private readonly Scene2 m_scene;

        protected readonly List<ITickable> m_tickables;
        protected IViewProvider m_viewProvider;

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;

        protected GameBase()
        {
            m_drawables = new List<IDrawable>();
            m_gameProviders = new List<IGameProvider>();
            m_widgets = new List<Widget>();
            m_bodies = new List<IBody>();
            m_tickables = new List<ITickable>();

            m_scene = new Scene2(400, 400, new ViewProviderBase());
            m_scene.AddDrawableProvider(this);
        }

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

        protected void SetViewProvider(IViewProvider _viewProvider)
        {
            m_scene.SetViewProvider(_viewProvider);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            List<IDrawable> drawables = new List<IDrawable>();
            drawables.AddRange(m_drawables);

            IEnumerable<IDrawable> subGameDrawables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetDrawables());
            drawables.AddRange(subGameDrawables);

            return drawables;
        }

        public IEnumerable<ITickable> GetTickables()
        {
            List<ITickable> tickables = new List<ITickable>();
            tickables.AddRange(m_tickables);

            IEnumerable<ITickable> subGameTickables = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetTickables());
            tickables.AddRange(subGameTickables);

            return tickables;
        }
        
        public virtual void Dispose()
        {
            m_scene.Dispose();
        }

        public IEnumerable<Widget> GetWidgets()
        {
            List<Widget> widgets = new List<Widget>();
            widgets.AddRange(m_widgets);

            IEnumerable<Widget> subGameWidgets = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetWidgets());
            widgets.AddRange(subGameWidgets);

            return widgets;
        }

        public IEnumerable<IBody> GetBodies()
        {
            List<IBody> bodies = new List<IBody>();
            bodies.AddRange(m_bodies);

            IEnumerable<IBody> subGameBodies = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetBodies());
            bodies.AddRange(subGameBodies);

            return bodies;
        }
        
        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_scene.Draw(_target, _states);
        }
    }
}