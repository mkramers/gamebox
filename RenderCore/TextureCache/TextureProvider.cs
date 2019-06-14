using Common.Cache;
using SFML.Graphics;

namespace RenderCore.TextureCache
{
    public class TextureProvider : ICacheObjectProvider<Texture, ITextureArgs>
    {
        public Texture GetObject(ITextureArgs _args)
        {
            Texture texture = _args.GetTexture();
            return texture;
        }
    }
}