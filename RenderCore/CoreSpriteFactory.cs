using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class CoreSpriteFactory
    {
        public CoreSprite GetSprite(ResourceId _resourceId, IntRect _textureCrop)
        {
            ResourceFactory resourceFactory = new ResourceFactory();
            Texture texture = resourceFactory.GetTexture(_resourceId);
            Sprite sprite = new Sprite(texture, _textureCrop)
            {
                Scale = new Vector2f(1.0f / texture.Size.X, 1.0f / texture.Size.Y)
            };
            CoreSprite coreSprite = new CoreSprite(sprite);
            return coreSprite;
        }
    }
}