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
    public class TextureFileLoader : ResourceLoaderBase<Texture>
    {
        public TextureFileLoader(string _textureFilePath) : base(_textureFilePath)
        {
        }

        public override Texture Load()
        {
            return new Texture(m_resourcePath);
        }
    }
}