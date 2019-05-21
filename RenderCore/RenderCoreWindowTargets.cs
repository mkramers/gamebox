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
        void SetViewProvider(IViewProvider _viewProvider);
        IViewProvider GetViewProvider();
    }

    public interface IRenderObjectContainer
    {
        void AddDrawable(IDrawable _drawable);
    }

    public class RenderObjectContainer : IRenderObjectContainer
    {
        private readonly BlockingCollection<IDrawable> m_drawables;

        public RenderObjectContainer()
        {
            m_drawables = new BlockingCollection<IDrawable>();
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
    }

    public class RenderCoreTarget : IRenderCoreTarget
    {
        private readonly Color m_clearColor;
        private RenderTexture m_renderTexture;
        private readonly RenderObjectContainer m_renderObjectContainer;
        private IViewProvider m_viewProvider;
        
        public RenderCoreTarget(Vector2u _size, Color _clearColor)
        {
            m_clearColor = _clearColor;

            m_viewProvider = new ViewProviderBase();

            SetSize(_size);

            m_renderTexture = new RenderTexture(_size.X, _size.Y);

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
            //m_renderTexture = new RenderTexture(_size.X, _size.Y);

            //m_viewProvider.SetSize(new Vector2f(_size.X * RATIO, _size.Y * RATIO));
        }

        public void SetViewProvider(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;

            //Vector2u size = m_renderTexture.Size;
            //m_viewProvider.SetSize(new Vector2f(size.X * RATIO, size.Y * RATIO));
        }

        public void AddDrawable(IDrawable _drawable)
        {
            m_renderObjectContainer.AddDrawable(_drawable);
        }
        
        public IViewProvider GetViewProvider()
        {
            return m_viewProvider;
        }

        public void Tick(TimeSpan _elapsed)
        {
            View view = m_viewProvider.GetView();
            m_renderTexture.SetView(view);
        }
    }
}