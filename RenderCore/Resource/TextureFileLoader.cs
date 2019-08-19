using SFML.Graphics;

namespace RenderCore.Resource
{
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