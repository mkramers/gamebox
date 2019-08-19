using SFML.Graphics;

namespace RenderCore.Resource
{
    public sealed class TextureResourceLoaderFactory : IResourceLoaderFactory<Texture>
    {
        public IResourceLoader<Texture> CreateResourceLoader(string _resourcePath)
        {
            return new TextureFileLoader(_resourcePath);
        }
    }
}