using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class CoreSpriteFactory
    {
        public BodySprite GetBodySprite(ResourceId _resourceId, float _mass)
        {
            ResourceFactory resourceFactory = new ResourceFactory();
            Texture texture = resourceFactory.GetTexture(_resourceId);

            BodySprite sprite = new BodySprite(texture, _mass)
            {
                Scale = new Vector2f(1.0f / texture.Size.X, 1.0f / texture.Size.Y)
            };
            return sprite;
        }
    }
}