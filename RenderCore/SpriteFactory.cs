using SFML.Graphics;

namespace RenderCore
{
    public class SpriteFactory
    {
        public Sprite GetSprite(ResourceId _resourceId, IntRect _textureCrop)
        {
            ResourceFactory resourceFactory = new ResourceFactory();
            Texture texture = resourceFactory.GetTexture(_resourceId);
            Sprite sprite = new Sprite(texture, _textureCrop);
            return sprite;
        }
    }
}