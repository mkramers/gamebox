using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public interface IDrawable : Drawable
    {
        void SetRenderPosition(Vector2 _position);
    }

    public class MultiDrawable : List<Drawable>, Drawable
    {
        protected MultiDrawable(IEnumerable<Drawable> _drawables) : base(_drawables)
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (Drawable drawable in this)
            {
                _target.Draw(drawable, _states);
            }
        }
    }

    public abstract class DrawableBase : IDrawable
    {
        private readonly Drawable m_drawable;

        protected DrawableBase(Drawable _drawable)
        {
            m_drawable = _drawable;
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable.Draw(_target, _states);
        }

        public abstract void SetRenderPosition(Vector2 _position);
    }

    public abstract class MultiDrawableBase<T> : List<T>, IDrawable where T : IDrawable
    {
        protected MultiDrawableBase(IEnumerable<T> _drawables) : base(_drawables)
        {
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            foreach (T extraDrawable in this)
            {
                extraDrawable.Draw(_target, _states);
            }
        }

        public abstract void SetRenderPosition(Vector2 _position);
    }
    
    public class SpriteDrawable : DrawableBase
    {
        private readonly Sprite m_sprite;

        public SpriteDrawable(Sprite _sprite) : base(_sprite)
        {
            m_sprite = _sprite;
        }

        public override void SetRenderPosition(Vector2 _position)
        {
            m_sprite.Position = _position.GetVector2F();
        }
    }

    public class MultiSpriteDrawable : MultiDrawableBase<SpriteDrawable>
    {
        public MultiSpriteDrawable(IEnumerable<SpriteDrawable> _sprites) : base(_sprites)
        {
        }

        public override void SetRenderPosition(Vector2 _position)
        {
            foreach (SpriteDrawable spriteDrawable in this)
            {
                spriteDrawable.SetRenderPosition(_position);
            }
        }
    }
}