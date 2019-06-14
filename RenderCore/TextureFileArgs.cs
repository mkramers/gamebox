using SFML.Graphics;

namespace RenderCore
{
    public class TextureFileArgs : ITextureArgs
    {
        private string FileName { get; }
        public IntRect? Area { get; }

        public TextureFileArgs(string _fileName, IntRect? _area = null)
        {
            FileName = _fileName;
            Area = _area;
        }

        public Texture GetTexture()
        {
            Texture texture = Area != null
                ? new Texture(FileName, Area.Value)
                : new Texture(FileName);
            return texture;
        }

        public bool Equals(ITextureArgs _other)
        {
            TextureFileArgs otherTextureArgs = _other as TextureFileArgs;
            if (otherTextureArgs == null)
            {
                return false;
            }

            return Area.Equals(otherTextureArgs.Area) && FileName.Equals(otherTextureArgs.FileName);
        }
    }
}