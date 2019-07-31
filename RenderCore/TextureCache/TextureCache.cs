using Common.Cache;
using SFML.Graphics;

namespace RenderCore.TextureCache
{
    public class TextureCache : Cache<Texture, ITextureArgs>
    {
        private TextureCache(ICacheObjectProvider<Texture, ITextureArgs> _cacheObjectProvider) : base(
            _cacheObjectProvider)
        {
        }

        public static TextureCache Instance { get; } = Factory.CreateTextureCache();

        public Texture GetTextureFromFile(string _fileName, IntRect? _area = null)
        {
            TextureFileArgs args = new TextureFileArgs(_fileName, _area);
            Texture texture = GetObject(args);
            return texture;
        }

        private static class Factory
        {
            internal static TextureCache CreateTextureCache()
            {
                TextureProvider textureProvider = new TextureProvider();
                return new TextureCache(textureProvider);
            }
        }
    }
}