using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class DrawableBase : IDrawable
    {
        private readonly Drawable m_drawable;

        protected DrawableBase(Drawable _drawable)
        {
            m_drawable = _drawable;
        }

        public virtual void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable.Draw(_target, _states);
        }

        public abstract void SetRenderPosition(Vector2 _positionScreen);
        public abstract void Dispose();
    }
}