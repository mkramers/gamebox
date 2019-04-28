using SFML.System;

namespace RenderCore
{
    public class ManBodyFactory
    {
        public BodySprite GetMan(float _mass)
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            BodySprite sprite = spriteFactory.GetBodySprite(ResourceId.MAN, _mass);
            Vector2u textureSize = sprite.Texture.Size;
            sprite.Scale = new Vector2f(1.0f / textureSize.X, 1.0f / textureSize.Y);
            return sprite;
        }
    }
}