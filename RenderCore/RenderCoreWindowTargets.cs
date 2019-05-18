using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IRenderCoreTarget : IDrawable, ITickable, IRenderObjectContainer
    {
        void SetSize(Vector2u _size);
        void SetView(View _view);
    }

    public interface IRenderObjectContainer
    {
        void AddDrawable(IDrawable _drawable);
        void AddWidget(IWidget _widget);
    }

    public class RenderObjectContainer : IRenderObjectContainer
    {
        private readonly BlockingCollection<IDrawable> m_drawables;
        private readonly TickableContainer<IWidget> m_widgets;

        public RenderObjectContainer()
        {
            m_drawables = new BlockingCollection<IDrawable>();
            m_widgets = new TickableContainer<IWidget>();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (IDrawable drawable in m_drawables)
            {
                _target.Draw(drawable, _states);
            }
        }

        public void Dispose()
        {
            m_widgets.Clear();

            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Dispose();
            }
            m_drawables.Clear();
        }

        public void AddDrawable(IDrawable _drawable)
        {
            m_drawables.Add(_drawable);
        }

        public void AddWidget(IWidget _widget)
        {
            m_drawables.Add(_widget);
            m_widgets.Add(_widget);
        }

        public void Tick(TimeSpan _elapsed)
        {
            m_widgets.Tick(_elapsed);
        }
    }

    public class RenderCoreTarget : IRenderCoreTarget
    {
        private readonly Color m_clearColor;
        private RenderTexture m_renderTexture;
        private readonly RenderObjectContainer m_renderObjectContainer;

        public RenderCoreTarget(Vector2u _size, Color _clearColor)
        {
            m_clearColor = _clearColor;

            SetSize(_size);
            m_renderObjectContainer = new RenderObjectContainer();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_renderTexture.Clear(m_clearColor);

            m_renderObjectContainer.Draw(m_renderTexture, _states);

            m_renderTexture.Display();

            Texture texture = m_renderTexture.Texture;
            _target.Draw(texture, _states);
        }

        public void Dispose()
        {
            m_renderTexture.Dispose();
            m_renderObjectContainer.Dispose();
        }

        public void SetSize(Vector2u _size)
        {
            m_renderTexture = new RenderTexture(_size.X, _size.Y);
        }

        public void SetView(View _view)
        {
            m_renderTexture.SetView(_view);
        }

        public void AddDrawable(IDrawable _drawable)
        {
            m_renderObjectContainer.AddDrawable(_drawable);
        }

        public void AddWidget(IWidget _widget)
        {
            m_renderObjectContainer.AddWidget(_widget);
        }

        public void Tick(TimeSpan _elapsed)
        {
            m_renderObjectContainer.Tick(_elapsed);
        }
    }
}