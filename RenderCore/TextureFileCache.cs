using Common.Cache;
using SFML.Graphics;

namespace RenderCore
{
    public class TextureFileCache : Cache<Texture, ITextureArgs>
    {
        private TextureFileCache(ICacheObjectProvider<Texture, ITextureArgs> _cacheObjectProvider) : base(_cacheObjectProvider)
        {
        }

        public Texture GetTextureFromFile(string _fileName, IntRect? _area = null)
        {
            TextureFileArgs args = new TextureFileArgs(_fileName, _area);
            Texture texture = GetObject(args);
            return texture;
        }

        public Texture GetTextureFromResource(string _resourceName, IntRect? _area = null)
        {
            TextureResourceArgs args = new TextureResourceArgs(_resourceName, _area);
            Texture texture = GetObject(args);
            return texture;
        }

        public static TextureFileCache Instance { get; } = Factory.CreateTextureCache();

        private static class Factory
        {
            internal static TextureFileCache CreateTextureCache()
            {
                TextureProvider textureProvider = new TextureProvider();
                return new TextureFileCache(textureProvider);
            }
        }

    }
}
