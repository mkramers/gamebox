using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ShapeDrawable : IDrawable
    {
        private readonly Shape m_shape;

        public ShapeDrawable(Shape _shape)
        {
            m_shape = _shape;
        }

        public void SetRenderPosition(Vector2 _position)
        {
            m_shape.Position = _position.GetVector2F();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_shape, _states);
        }
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
}