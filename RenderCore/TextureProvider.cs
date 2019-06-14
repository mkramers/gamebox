using SFML.Graphics;

namespace RenderCore
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