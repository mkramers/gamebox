﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Render;
using RenderCore.ViewProvider;
using RenderCore.Widget;
using SFML.Graphics;
using TGUI;

namespace GameCore
{
    public abstract class GameBase : IGame
    {
        protected readonly List<IDrawable> m_drawables;
        private readonly List<IGameProvider> m_gameProviders;
        protected readonly List<IGuiWidget> m_widgets;
        protected readonly List<IBody> m_bodies;
        private readonly ISceneProvider m_sceneProvider;

        protected readonly List<ITickable> m_tickables;
        protected IViewProvider m_viewProvider;

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;
        
        protected GameBase(ISceneProvider _sceneProvider)
        {
            m_drawables = new List<IDrawable>();
            m_gameProviders = new List<IGameProvider>();
            m_widgets = new List<IGuiWidget>();
            m_bodies = new List<IBody>();
            m_tickables = new List<ITickable>();

            m_sceneProvider = _sceneProvider;
            m_sceneProvider.AddDrawableProvider(this);
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
        
        protected void SetViewProvider(IViewProvider _viewProvider)
        {
            m_sceneProvider.SetViewProvider(_viewProvider);
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
            m_sceneProvider.Dispose();
        }

        public IEnumerable<IGuiWidget> GetWidgets()
        {
            List<IGuiWidget> widgets = new List<IGuiWidget>();
            widgets.AddRange(m_widgets);

            IEnumerable<IGuiWidget> subGameWidgets = m_gameProviders.SelectMany(_gameProvider => _gameProvider.GetWidgets());
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

        public Texture GetTexture()
        {
            Texture texture = m_sceneProvider.GetTexture();
            return texture;
        }

        public void SetSize(uint _width, uint _height)
        {
            m_sceneProvider.SetSize(_width, _height);
        }
    }
}