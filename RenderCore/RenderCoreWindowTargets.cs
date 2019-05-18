using System;
using System.Collections.Concurrent;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IRenderCoreTarget : IDrawable
    {
        void SetSize(Vector2u _size);
        void SetView(View _view);
        RenderTarget GetRenderTarget();
        void AddDrawable(IDrawable _drawable);
    }

    public class RenderCoreTarget : BlockingCollection<IDrawable>, IRenderCoreTarget
    {
        private readonly Color m_clearColor;
        private RenderTexture m_renderTexture;

        public RenderCoreTarget(Vector2u _size, Color _clearColor)
        {
            m_clearColor = _clearColor;
            SetSize(_size);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            Texture texture = m_renderTexture.Texture;

            m_renderTexture.Clear(m_clearColor);

            foreach (IDrawable drawable in this)
            {
                m_renderTexture.Draw(drawable, _states);
            }

            m_renderTexture.Display();

            _target.Draw(texture, _states);
        }

        protected override void Dispose(bool _disposing)
        {
            base.Dispose(_disposing);
            
            m_renderTexture.Dispose();

            foreach (IDrawable drawable in this)
            {
                drawable.Dispose();
            }

            this.Clear();
        }

        public void SetSize(Vector2u _size)
        {
            m_renderTexture = new RenderTexture(_size.X, _size.Y);
        }

        public void SetView(View _view)
        {
            m_renderTexture.SetView(_view);
        }

        public RenderTarget GetRenderTarget()
        {
            return m_renderTexture;
        }

        public void AddDrawable(IDrawable _drawable)
        {
            Add(_drawable);
        }
    }
}