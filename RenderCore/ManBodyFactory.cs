using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ManBodyFactory
    {
        public IBody GetManBody()
        {
            CoreSpriteFactory spriteFactory = new CoreSpriteFactory();
            IntRect textureCrop = new IntRect(50, 24, 24, 24);
            Sprite sprite = spriteFactory.GetSprite(ResourceId.MAN, textureCrop);
            sprite.Scale = new Vector2f(1.0f / textureCrop.Width, 1.0f / textureCrop.Height);
            RigidBody body = new RigidBody(sprite);
            return body;
        }
    }
}