using SFML.Graphics;

namespace RenderCore
{
    public class TextureResourceArgs : ITextureArgs
    {
        public bool Equals(ITextureArgs _other)
        {
            TextureResourceArgs otherTextureArgs = _other as TextureResourceArgs;
            if (otherTextureArgs == null)
            {
                return false;
            }

            return Area.Equals(otherTextureArgs.Area) && ResourceName.Equals(otherTextureArgs.ResourceName);
        }

        public IntRect? Area { get; }

        public string ResourceName { get; }

        public TextureResourceArgs(string _resourceName, IntRect? _area = null)
        {
            ResourceName = _resourceName;
            Area = _area;
        }

        public Texture GetTexture()
        {
            byte[] resourceData = ResourceUtilities.GetResourceData(ResourceName);

            Image image = new Image(resourceData);

            Texture texture = Area != null
                ? new Texture(image, Area.Value)
                : new Texture(image);
            return texture;
        }
    }
}