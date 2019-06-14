using SFML.Graphics;

namespace RenderCore
{
    public abstract class TextureCache : Cache<Texture, ITextureArgs>
    {
        protected TextureCache(ICacheObjectProvider<Texture, ITextureArgs> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

    }
}