using SFML.Graphics;

namespace RenderCore.Resource
{
    public class TextureResourceLoaderFactory : IResourceLoaderFactory<Texture>
    {
        public virtual IResourceLoader<Texture> CreateResourceLoader(string _resourcePath)
        {
            return new TextureFileLoader(_resourcePath);
        }
    }
}