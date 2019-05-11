﻿using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class SpriteDrawable : DrawableBase
    {
        private readonly Sprite m_sprite;

        public SpriteDrawable(Sprite _sprite) : base(_sprite)
        {
            m_sprite = _sprite;
        }

        public override void SetRenderPosition(Vector2 _positionScreen)
        {
            m_sprite.Position = _positionScreen.GetVector2F();
        }

        public override void Dispose()
        {
            m_sprite.Dispose();
        }
    }
}