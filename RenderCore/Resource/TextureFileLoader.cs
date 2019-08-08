using SFML.Graphics;

namespace RenderCore.Resource
{
    public class TextureFileLoader : IResourceLoader<Texture>
    {
        private readonly string m_textureFilePath;

        public TextureFileLoader(string _textureFilePath)
        {
            m_textureFilePath = _textureFilePath;
        }

        public Texture Load()
        {
            return new Texture(m_textureFilePath);
        }
    }
}