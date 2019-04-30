using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class CoreSpriteFactory
    {
        public Sprite GetBodySprite(ResourceId _resourceId)
        {
            ResourceFactory resourceFactory = new ResourceFactory();
            Texture texture = resourceFactory.GetTexture(_resourceId);

            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f(1.0f / texture.Size.X, 1.0f / texture.Size.Y)
            };

            return sprite;
        }
    }
}